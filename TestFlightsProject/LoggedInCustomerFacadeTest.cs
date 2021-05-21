using FlightsProject;
using FlightsProject.DAO_PGSQL;
using FlightsProject.Facade;
using FlightsProject.Login;
using FlightsProject.POCO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;

namespace TestFlightsProject
{
    [TestClass]
    public class LoggedInCustomerFacadeTest
    {
        UserDAOPGSQL userDAOPGSQL = new UserDAOPGSQL();
        FlightDAOPGSQL flightDAOPGSQL = new FlightDAOPGSQL();
        AirlineCompanyDAOPGSQL airlineCompanyDAOPGSQL = new AirlineCompanyDAOPGSQL();
        TicketDAOPGSQL ticketDAOPGSQL = new TicketDAOPGSQL();

        public User CreateAirlineUserForTest()
        {
            User airlineUser = new User
            {
                Username = TestData.AnonymouseFacade_CreateAirlineUser_Username,
                Password = TestData.AnonymouseFacade_CreateAirlineUser_Password,
                Email = TestData.AnonymouseFacade_CreateAirlineUser_Email,
                User_Role = TestData.AnonymouseFacade_CreateAirlineUser_User_Role
            };
            return airlineUser;
        }

        public AirlineCompany CreateAirlineCompanyForTest()
        {
            userDAOPGSQL.Add(CreateAirlineUserForTest());
            var u = userDAOPGSQL.GetByUserName(TestData.AnonymouseFacade_CreateAirlineUser_Username);
            TestData.AnonymouseFacade_CreateAirlineUser_Id = (int)u.Id;

            AirlineCompany airlineCompany = new AirlineCompany
            {
                Name = TestData.AnonymouseFacade_CreateAirlineCompany_Name,
                Country_Id = TestData.AnonymouseFacade_CreateAirlineCompany_Country_Id,
                User_Id = TestData.AnonymouseFacade_CreateAirlineUser_Id,
            };
            return airlineCompany;
        }

        public Flight CreateFlightForTest()
        {
            airlineCompanyDAOPGSQL.Add(CreateAirlineCompanyForTest());

            var ac = airlineCompanyDAOPGSQL.GetAirlineByUserame(TestData.AnonymouseFacade_CreateAirlineUser_Username);
            TestData.AnonymouseFacade_CreateAirlineCompany_Id = ac.Id;

            Flight flight = new Flight
            {
                Airline_Company_Id = TestData.AnonymouseFacade_CreateAirlineCompany_Id,
                Origin_Country_Id = TestData.AnonymouseFacade_CreateFlight_OriginCountryId,
                Destination_Country_Id = TestData.AnonymouseFacade_CreateFlight_DestinationCountryId,
                Departure_Time = TestData.AnonymouseFacade_CreateFlight_DepartureTime,
                Landing_Time = TestData.AnonymouseFacade_CreateFlight_LandingTime,
                Tickets_Remaining = TestData.AnonymouseFacade_CreateFlight_TicketsRemaining
            };
            return flight;
        }

        public User CreateCustomerUserForTest()
        {
            User airlineUser = new User
            {
                Username = TestData.AdminFacade_CreateCustomerUser_Username,
                Password = TestData.AdminFacade_CreateCustomerUser_Password,
                Email = TestData.AdminFacade_CreateCustomerUser_Email,
                User_Role = TestData.AdminFacade_CreateCustomerUser_UserRole
            };
            return airlineUser;
        }

        public Customer CreateCustomerForTest()
        {
            userDAOPGSQL.Add(CreateCustomerUserForTest());
            var u = userDAOPGSQL.GetByUserName(TestData.AdminFacade_CreateCustomerUser_Username);
            TestData.AdminFacade_CreateCustomerUser_Id = (int)u.Id;

            Customer customer = new Customer()
            {
                First_Name = TestData.AdminFacade_CreateCustomer_FirstName,
                Last_Name = TestData.AdminFacade_CreateCustomer_LastName,
                Address = TestData.AdminFacade_CreateCustomer_Address,
                Phone_No = TestData.AdminFacade_CreateCustomer_PhoneNo,
                Credit_Card_No = TestData.AdminFacade_CreateCustomer_CreditCardNo,
                User_Id = TestData.AdminFacade_CreateCustomerUser_Id
            };
            return customer;
        }

        public void getTokenAndGetFacade(out LoginToken<Admin> tokenAdmin, out LoggedInAdministratorFacade facadeAdmin,
            out LoginToken<Customer> tokenCustomer, out LoggedInCustomerFacade fasadeCustomer)
        {
            ILoginService loginService = new LoginService();
            loginService.TryAdminLogin(FlightCenterConfig.ADMIN_NAME, FlightCenterConfig.ADMIN_PASSWORD, out tokenAdmin);
            facadeAdmin = FlightsCenterSystem.GetInstance().GetFacade(tokenAdmin) as LoggedInAdministratorFacade;

            facadeAdmin.CreateNewCustomer(tokenAdmin, CreateCustomerForTest());
            tokenCustomer = new LoginToken<Customer>()
            {
                User = facadeAdmin.GetAllCustomers(tokenAdmin)[0]
            };
            fasadeCustomer = FlightsCenterSystem.GetInstance().GetFacade(tokenCustomer) as LoggedInCustomerFacade;
        }

        [TestMethod]
        public void CancelTicketTest()
        {
            TestData.DeleteAllData();
            getTokenAndGetFacade(out LoginToken<Admin> tokenAdmin, out LoggedInAdministratorFacade facadeAdmin,
                                               out LoginToken<Customer> tokenCustomer, out LoggedInCustomerFacade fasadeCustomer);
            flightDAOPGSQL.Add(CreateFlightForTest());
            var f = flightDAOPGSQL.GetAll()[0];
            fasadeCustomer.PurchaseTicket(tokenCustomer, f);
            var t = ticketDAOPGSQL.GetAll()[0];
            fasadeCustomer.CancelTicket(tokenCustomer, t, f);

            Assert.AreEqual(flightDAOPGSQL.GetAll()[0].Tickets_Remaining, TestData.AnonymouseFacade_CreateFlight_TicketsRemaining);
            Assert.AreEqual(null, ticketDAOPGSQL.Get((int)t.Id));
        }
        [TestMethod]
        public void GetAllMyFlightsTest()
        {
            TestData.DeleteAllData();
            getTokenAndGetFacade(out LoginToken<Admin> tokenAdmin, out LoggedInAdministratorFacade facadeAdmin,
                                               out LoginToken<Customer> tokenCustomer, out LoggedInCustomerFacade fasadeCustomer);
            flightDAOPGSQL.Add(CreateFlightForTest());
            var f = flightDAOPGSQL.GetAll()[0];
            Ticket t = fasadeCustomer.PurchaseTicket(tokenCustomer, f);
            var f_list = fasadeCustomer.GetAllMyFlights(tokenCustomer);

            Assert.AreEqual(1, f_list.Count); 

        }
        [TestMethod]
        public void PurchaseTicketTest()
        {
            TestData.DeleteAllData();
            getTokenAndGetFacade(out LoginToken<Admin> tokenAdmin, out LoggedInAdministratorFacade facadeAdmin,
                                               out LoginToken<Customer> tokenCustomer, out LoggedInCustomerFacade fasadeCustomer);
            flightDAOPGSQL.Add(CreateFlightForTest());
            var f = flightDAOPGSQL.GetAll()[0];
            Ticket t = fasadeCustomer.PurchaseTicket(tokenCustomer, f);

            Assert.AreNotEqual(t, null);
            Assert.AreEqual(t.Id_Flight, f.Id);
            Assert.AreEqual(f.Tickets_Remaining, TestData.AnonymouseFacade_CreateFlight_TicketsRemaining-1);
        }
    }
}
