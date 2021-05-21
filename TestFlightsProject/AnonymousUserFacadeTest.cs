using FlightsProject;
using FlightsProject.DAO_PGSQL;
using FlightsProject.Facade;
using FlightsProject.I_Facade;
using FlightsProject.Login;
using FlightsProject.POCO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using System;

namespace TestFlightsProject
{
    [TestClass]
    public class AnonymousUserFacadeTest
    {

        UserDAOPGSQL userDAOPGSQL = new UserDAOPGSQL();
        FlightDAOPGSQL flightDAOPGSQL = new FlightDAOPGSQL();
        AirlineCompanyDAOPGSQL airlineCompanyDAOPGSQL = new AirlineCompanyDAOPGSQL();

        public User CreateAirlineUserForTest() {
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


        [TestMethod]
        public void GetAllAirlineCompaniesTest()
        {
            AnonymousUserFacade facade = FlightsCenterSystem.GetInstance().GetFacade<Anonymous>(null) as AnonymousUserFacade;
            TestData.DeleteAllData();

            airlineCompanyDAOPGSQL.Add(CreateAirlineCompanyForTest());

            var ac_list = facade.GetAllAirlineCompanies();

            Assert.AreNotEqual(ac_list, null);
            Assert.AreEqual(ac_list.Count, 1);
        }

        [TestMethod]
        public void GetAllFlightsTest()
        {
            AnonymousUserFacade facade = FlightsCenterSystem.GetInstance().GetFacade<Anonymous>(null) as AnonymousUserFacade;
            TestData.DeleteAllData();

            flightDAOPGSQL.Add(CreateFlightForTest());

            var f_list = facade.GetAllFlights();

            Assert.AreNotEqual(f_list, null);
            Assert.AreEqual(f_list.Count, 1);
        }

        [TestMethod]
        public void GetAllFlightsVacancyTest()
        {
            AnonymousUserFacade facade = FlightsCenterSystem.GetInstance().GetFacade<Anonymous>(null) as AnonymousUserFacade;
            TestData.DeleteAllData();

            flightDAOPGSQL.Add(CreateFlightForTest());

            var f_list = facade.GetAllFlightsVacancy();

            Assert.AreNotEqual(f_list, null);
            Assert.AreEqual(f_list.Count, 1);

        }

        [TestMethod]
        public void GetFlightByIdTest()
        {
            AnonymousUserFacade facade = FlightsCenterSystem.GetInstance().GetFacade<Anonymous>(null) as AnonymousUserFacade;
            TestData.DeleteAllData();

            flightDAOPGSQL.Add(CreateFlightForTest());

            var flights_list = flightDAOPGSQL.GetAll();

            Flight f = facade.GetFlightById((int)flights_list[0].Id);

            Assert.AreNotEqual(f, null);
            Assert.AreEqual(TestData.CreateAirlineCompany_Id, f.Airline_Company_Id);
            Assert.AreEqual(TestData.CreateFlight_OriginCountryId, f.Origin_Country_Id);
            Assert.AreEqual(TestData.CreateFlight_DestinationCountryId, f.Destination_Country_Id);
            Assert.AreEqual(TestData.CreateFlight_DepartureTime.AddHours(10), f.Departure_Time);
            Assert.AreEqual(TestData.CreateFlight_LandingTime.AddHours(10), f.Landing_Time);
            Assert.AreEqual(TestData.CreateFlight_TicketsRemaining, f.Tickets_Remaining);
        }
        [TestMethod]
        public void GetFlightsByDepatrureDateTest()
        {
            AnonymousUserFacade facade = FlightsCenterSystem.GetInstance().GetFacade<Anonymous>(null) as AnonymousUserFacade;
            TestData.DeleteAllData();

            flightDAOPGSQL.Add(CreateFlightForTest());

            var f_list = facade.GetFlightsByDepatrureDate(TestData.CreateFlight_DepartureTime);

            Assert.AreNotEqual(f_list, null);
            Assert.AreEqual(f_list.Count, 1);
        }
        [TestMethod]
        public void GetFlightsByDestinationCountryTest()
        {
            AnonymousUserFacade facade = FlightsCenterSystem.GetInstance().GetFacade<Anonymous>(null) as AnonymousUserFacade;
            TestData.DeleteAllData();
            flightDAOPGSQL.Add(CreateFlightForTest());
           
            var f_list = facade.GetFlightsByDestinationCountry(TestData.CreateFlight_DestinationCountryId);

            Assert.AreNotEqual(f_list, null);
            Assert.AreEqual(f_list.Count, 1);
        }

        [TestMethod]
        public void GetFlightsByLandingDateTest()
        {
            AnonymousUserFacade facade = FlightsCenterSystem.GetInstance().GetFacade<Anonymous>(null) as AnonymousUserFacade;
            TestData.DeleteAllData();
            flightDAOPGSQL.Add(CreateFlightForTest());

            var f_list = facade.GetFlightsByLandingDate(TestData.CreateFlight_LandingTime);

            Assert.AreNotEqual(f_list, null);
            Assert.AreEqual(f_list.Count, 1);
        }

        [TestMethod]
        public void GetFlightsByOriginCountryTest()
        {
            AnonymousUserFacade facade = FlightsCenterSystem.GetInstance().GetFacade<Anonymous>(null) as AnonymousUserFacade;
            TestData.DeleteAllData();
            flightDAOPGSQL.Add(CreateFlightForTest());

            var flights_list = flightDAOPGSQL.GetAll();
            var f_list = facade.GetFlightsByOriginCountry(flights_list[0].Origin_Country_Id);

            Assert.AreNotEqual(f_list, null);
            Assert.AreEqual(f_list.Count, 1);
        }
    }
}
