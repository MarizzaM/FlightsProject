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
            airlineCompanyDAOPGSQL.Add(CreateAirlineCompanyForTest());

            var ac = airlineCompanyDAOPGSQL.GetAirlineByUserame(TestData.CreateAirlineUser_Username);
            TestData.CreateAirlineCompany_Id = ac.Id;

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

            Assert.AreEqual(flightDAOPGSQL.GetAll()[0].Tickets_Remaining, TestData.CreateFlight_TicketsRemaining);
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
            Assert.AreEqual(f.Tickets_Remaining, TestData.CreateFlight_TicketsRemaining-1);
        }
    }
}
