using FlightsProject;
using FlightsProject.DAO_PGSQL;
using FlightsProject.Facade;
using FlightsProject.Login;
using FlightsProject.POCO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestFlightsProject
{
    [TestClass]
    public class LoggedsInAirlineFacadeTest
    {
        UserDAOPGSQL userDAOPGSQL = new UserDAOPGSQL();
        FlightDAOPGSQL flightDAOPGSQL = new FlightDAOPGSQL();
        AirlineCompanyDAOPGSQL airlineCompanyDAOPGSQL = new AirlineCompanyDAOPGSQL();
        TicketDAOPGSQL ticketDAOPGSQL = new TicketDAOPGSQL();

        public User CreateAirlineUserForTest()
        {
            User airlineUser = new User
            {
                Username = TestData.CreateAirlineUser_Username,
                Password = TestData.CreateAirlineUser_Password,
                Email = TestData.CreateAirlineUser_Email,
                User_Role = TestData.CreateAirlineUser_UserRole
            };
            return airlineUser;
        }

        public AirlineCompany CreateAirlineCompanyForTest()
        {
            userDAOPGSQL.Add(CreateAirlineUserForTest());
            var u = userDAOPGSQL.GetByUserName(TestData.CreateAirlineUser_Username);
            TestData.CreateAirlineUser_Id = (int)u.Id;

            AirlineCompany airlineCompany = new AirlineCompany
            {
                Name = TestData.CreateAirlineCompany_Name,
                Country_Id = TestData.CreateAirlineCompany_Country_Id,
                User_Id = TestData.CreateAirlineUser_Id,
            };
            return airlineCompany;
        }

        public Flight CreateFlightForTest()
        {
            AirlineCompany airline = airlineCompanyDAOPGSQL.GetAirlineByUserame(TestData.CreateAirlineUser_Username);

            TestData.CreateAirlineCompany_Id = airline.Id;

            Flight flight = new Flight
            {
                Airline_Company_Id = TestData.CreateAirlineCompany_Id,
                Origin_Country_Id = TestData.CreateFlight_OriginCountryId,
                Destination_Country_Id = TestData.CreateFlight_DestinationCountryId,
                Departure_Time = TestData.CreateFlight_DepartureTime,
                Landing_Time = TestData.CreateFlight_LandingTime,
                Tickets_Remaining = TestData.CreateFlight_TicketsRemaining
            };
            return flight;
        }

        public User CreateCustomerUserForTest()
        {
            User airlineUser = new User
            {
                Username = TestData.CreateCustomerUser_Username,
                Password = TestData.CreateCustomerUser_Password,
                Email = TestData.CreateCustomerUser_Email,
                User_Role = TestData.CreateCustomerUser_UserRole
            };
            return airlineUser;
        }

        public Customer CreateCustomerForTest()
        {
            userDAOPGSQL.Add(CreateCustomerUserForTest());
            var u = userDAOPGSQL.GetByUserName(TestData.CreateCustomerUser_Username);
            TestData.CreateCustomerUser_Id = (int)u.Id;

            Customer customer = new Customer()
            {
                First_Name = TestData.CreateCustomer_FirstName,
                Last_Name = TestData.CreateCustomer_LastName,
                Address = TestData.CreateCustomer_Address,
                Phone_No = TestData.CreateCustomer_PhoneNo,
                Credit_Card_No = TestData.CreateCustomer_CreditCardNo,
                User_Id = TestData.CreateCustomerUser_Id
            };
            return customer;
        }

        public void getTokenAndGetFacade(out LoginToken<Admin> tokenAdmin, out LoggedInAdministratorFacade facadeAdmin,
            out LoginToken<AirlineCompany> tokenAirline, out LoggedsInAirlineFacade fasadeAirline)
        {
            AnonymousUserFacade facade = FlightsCenterSystem.GetInstance().GetFacade<Anonymous>(null) as AnonymousUserFacade;

            ILoginService loginService = new LoginService();
            loginService.TryAdminLogin(FlightCenterConfig.ADMIN_NAME, FlightCenterConfig.ADMIN_PASSWORD, out tokenAdmin);
            facadeAdmin = FlightsCenterSystem.GetInstance().GetFacade(tokenAdmin) as LoggedInAdministratorFacade;

            facadeAdmin.CreateNewAirline(tokenAdmin, CreateAirlineCompanyForTest());

            AirlineCompany airline = airlineCompanyDAOPGSQL.GetAirlineByUserame(TestData.CreateAirlineUser_Username);
            tokenAirline = new LoginToken<AirlineCompany>()
            {
                User = airline
            };
            fasadeAirline = FlightsCenterSystem.GetInstance().GetFacade(tokenAirline) as LoggedsInAirlineFacade;
        }
        [TestMethod]
        public void CancelFlightTest()
        {
            TestData.DeleteAllData();
            getTokenAndGetFacade(out LoginToken<Admin> tokenAdmin, out LoggedInAdministratorFacade facadeAdmin,
                                               out LoginToken<AirlineCompany> tokenAirline, out LoggedsInAirlineFacade fasadeAirline);

            AirlineCompany airline = airlineCompanyDAOPGSQL.GetAirlineByUserame(TestData.CreateAirlineUser_Username);
            TestData.CreateAirlineCompany_Id = airline.Id;

            fasadeAirline.CreateFlight(tokenAirline, CreateFlightForTest());
            var f = facadeAdmin.GetAllFlights()[0];

            fasadeAirline.CancelFlight(tokenAirline, f);

            Assert.AreNotEqual(1, flightDAOPGSQL.GetAll().Count);
            Assert.AreEqual(0, flightDAOPGSQL.GetAll().Count);
        }

        [TestMethod]
        public void ChangeMyPasswordTest()
        {
            TestData.DeleteAllData();
            TestData.DeleteAllData();
            getTokenAndGetFacade(out LoginToken<Admin> tokenAdmin, out LoggedInAdministratorFacade facadeAdmin,
                                               out LoginToken<AirlineCompany> tokenAirline, out LoggedsInAirlineFacade fasadeAirline);
            var ac = airlineCompanyDAOPGSQL.GetAirlineByUserame(tokenAirline.User.Username);

            System.Console.WriteLine(tokenAirline.User.Username);
            System.Console.WriteLine(tokenAirline.User.Password);
            System.Console.WriteLine(ac.Password);
            fasadeAirline.ChangeMyPassword(tokenAirline, ac.Password, TestData.AirlineUser_Password_new);

            System.Console.WriteLine(tokenAirline.User.Password);

            Assert.AreEqual(tokenAirline.User.Password, TestData.AirlineUser_Password_new);


        }


        [TestMethod]
        public void CreateFlightTest()
        {
            TestData.DeleteAllData();
            getTokenAndGetFacade(out LoginToken<Admin> tokenAdmin, out LoggedInAdministratorFacade facadeAdmin,
                                               out LoginToken<AirlineCompany> tokenAirline, out LoggedsInAirlineFacade fasadeAirline);

            fasadeAirline.CreateFlight(tokenAirline, CreateFlightForTest());
            var list = facadeAdmin.GetAllFlights();

            Assert.AreNotEqual(0, list.Count);
            Assert.AreEqual(1, list.Count);
        }
        [TestMethod]
        public void GetAllTicketsTest()
        {
            TestData.DeleteAllData();
            getTokenAndGetFacade(out LoginToken<Admin> tokenAdmin, out LoggedInAdministratorFacade facadeAdmin,
                       out LoginToken<AirlineCompany> tokenAirline, out LoggedsInAirlineFacade fasadeAirline);
            LoggedInCustomerFacadeTest loggedInCustomerFacadeTest = new LoggedInCustomerFacadeTest();
            loggedInCustomerFacadeTest.getTokenAndGetFacade(out tokenAdmin, out facadeAdmin,
            out LoginToken<Customer> tokenCustomer, out LoggedInCustomerFacade fasadeCustomer);


            fasadeAirline.CreateFlight(tokenAirline, CreateFlightForTest());
            var f = fasadeAirline.GetAllFlights(tokenAirline)[0];
            fasadeCustomer.PurchaseTicket(tokenCustomer, f);
            var list = fasadeAirline.GetAllTickets(tokenAirline);

            Assert.AreNotEqual(0, list.Count);
            Assert.AreEqual(1, list.Count);

        }
        [TestMethod]
        public void MofidyAirlineDetailsTest()
        {
            TestData.DeleteAllData();
            getTokenAndGetFacade(out LoginToken<Admin> tokenAdmin, out LoggedInAdministratorFacade facadeAdmin,
                                   out LoginToken<AirlineCompany> tokenAirline, out LoggedsInAirlineFacade fasadeAirline);
            var ac = airlineCompanyDAOPGSQL.GetAirlineByUserame(tokenAirline.User.Username);

            ac.Name = TestData.UpdateAirlineCompany_Name;

            fasadeAirline.MofidyAirlineDetails(tokenAirline,ac);
            var ac_new = airlineCompanyDAOPGSQL.Get((int)ac.Id);

            Assert.AreEqual(TestData.UpdateAirlineCompany_Name, ac_new.Name);
        }
        [TestMethod]
        public void UpdateFlightTest()
        {

            TestData.DeleteAllData();
            getTokenAndGetFacade(out LoginToken<Admin> tokenAdmin, out LoggedInAdministratorFacade facadeAdmin,
                                               out LoginToken<AirlineCompany> tokenAirline, out LoggedsInAirlineFacade fasadeAirline);

            fasadeAirline.CreateFlight(tokenAirline, CreateFlightForTest());
            var f = facadeAdmin.GetAllFlights()[0];

            f.Departure_Time = TestData.UpdateFlight_DepartureTime;
            f.Landing_Time = TestData.UpdateFlight_LandingTime;
            f.Tickets_Remaining = TestData.UpdateFlight_TicketsRemaining;

            fasadeAirline.UpdateFlight(tokenAirline, f);
            Flight f_new = flightDAOPGSQL.GetFlightsById((int)f.Id);

            Assert.AreEqual( TestData.UpdateFlight_DepartureTime.AddHours(10), f_new.Departure_Time);
            Assert.AreEqual( TestData.UpdateFlight_LandingTime.AddHours(10), f_new.Landing_Time);
            Assert.AreEqual(TestData.UpdateFlight_TicketsRemaining, f_new.Tickets_Remaining, f_new.Tickets_Remaining);
        }
    }
}
