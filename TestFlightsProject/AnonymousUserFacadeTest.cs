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
        static string conn_string_test = "Host=localhost;Username=postgres;Password=336527981;Database=FlightsProjectDBTest";
        public void DeleteAllData()
        {
            using (var my_conn = new NpgsqlConnection(conn_string_test))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();

                cmd.Connection = my_conn;
                cmd.CommandText = $"DELETE FROM flights; DELETE FROM airline_companies; DELETE FROM administrators;  DELETE FROM users;";
                cmd.ExecuteNonQuery();
            }
        }
       
        UserDAOPGSQL userDAOPGSQL = new UserDAOPGSQL();
        FlightDAOPGSQL flightDAOPGSQL = new FlightDAOPGSQL();
        AirlineCompanyDAOPGSQL airlineCompanyDAOPGSQL = new AirlineCompanyDAOPGSQL();

        public User CreateAirlineUserForTest() {
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


        [TestMethod]
        public void GetAllAirlineCompaniesTest()
        {
            AnonymousUserFacade facade = FlightsCenterSystem.GetInstance().GetFacade<Anonymous>(null) as AnonymousUserFacade;
            DeleteAllData();

            airlineCompanyDAOPGSQL.Add(CreateAirlineCompanyForTest());

            var ac_list = facade.GetAllAirlineCompanies();

            Assert.AreNotEqual(ac_list, null);
            Assert.AreEqual(ac_list.Count, 1);
        }

        [TestMethod]
        public void GetAllFlightsTest()
        {
            AnonymousUserFacade facade = FlightsCenterSystem.GetInstance().GetFacade<Anonymous>(null) as AnonymousUserFacade;
            DeleteAllData();

            flightDAOPGSQL.Add(CreateFlightForTest());

            var f_list = facade.GetAllFlights();

            Assert.AreNotEqual(f_list, null);
            Assert.AreEqual(f_list.Count, 1);
        }

        [TestMethod]
        public void GetAllFlightsVacancyTest()
        {
            AnonymousUserFacade facade = FlightsCenterSystem.GetInstance().GetFacade<Anonymous>(null) as AnonymousUserFacade;
            DeleteAllData();

            flightDAOPGSQL.Add(CreateFlightForTest());

            var f_list = facade.GetAllFlightsVacancy();

            Assert.AreNotEqual(f_list, null);
            Assert.AreEqual(f_list.Count, 1);

        }

        [TestMethod]
        public void GetFlightByIdTest()
        {
            AnonymousUserFacade facade = FlightsCenterSystem.GetInstance().GetFacade<Anonymous>(null) as AnonymousUserFacade;
            DeleteAllData();

            flightDAOPGSQL.Add(CreateFlightForTest());

            var flights_list = flightDAOPGSQL.GetAll();

            Flight f = facade.GetFlightById((int)flights_list[0].Id);

            Assert.AreNotEqual(f, null);
            Assert.AreEqual(TestData.AnonymouseFacade_CreateAirlineCompany_Id, f.Airline_Company_Id);
            Assert.AreEqual(TestData.AnonymouseFacade_CreateFlight_OriginCountryId, f.Origin_Country_Id);
            Assert.AreEqual(TestData.AnonymouseFacade_CreateFlight_DestinationCountryId, f.Destination_Country_Id);
            Assert.AreEqual(TestData.AnonymouseFacade_CreateFlight_DepartureTime.AddHours(10), f.Departure_Time);
            Assert.AreEqual(TestData.AnonymouseFacade_CreateFlight_LandingTime.AddHours(10), f.Landing_Time);
            Assert.AreEqual(TestData.AnonymouseFacade_CreateFlight_TicketsRemaining, f.Tickets_Remaining);
        }
        [TestMethod]
        public void GetFlightsByDepatrureDateTest()
        {
            AnonymousUserFacade facade = FlightsCenterSystem.GetInstance().GetFacade<Anonymous>(null) as AnonymousUserFacade;
            DeleteAllData();

            flightDAOPGSQL.Add(CreateFlightForTest());

            var f_list = facade.GetFlightsByDepatrureDate(TestData.AnonymouseFacade_CreateFlight_DepartureTime);

            Assert.AreNotEqual(f_list, null);
            Assert.AreEqual(f_list.Count, 1);
        }
        [TestMethod]
        public void GetFlightsByDestinationCountryTest()
        {
            AnonymousUserFacade facade = FlightsCenterSystem.GetInstance().GetFacade<Anonymous>(null) as AnonymousUserFacade;
            DeleteAllData();
            flightDAOPGSQL.Add(CreateFlightForTest());
           
            var f_list = facade.GetFlightsByDestinationCountry(TestData.AnonymouseFacade_CreateFlight_DestinationCountryId);

            Assert.AreNotEqual(f_list, null);
            Assert.AreEqual(f_list.Count, 1);
        }

        [TestMethod]
        public void GetFlightsByLandingDateTest()
        {
            AnonymousUserFacade facade = FlightsCenterSystem.GetInstance().GetFacade<Anonymous>(null) as AnonymousUserFacade;
            DeleteAllData();
            flightDAOPGSQL.Add(CreateFlightForTest());

            var f_list = facade.GetFlightsByLandingDate(TestData.AnonymouseFacade_CreateFlight_LandingTime);

            Assert.AreNotEqual(f_list, null);
            Assert.AreEqual(f_list.Count, 1);
        }

        [TestMethod]
        public void GetFlightsByOriginCountryTest()
        {
            AnonymousUserFacade facade = FlightsCenterSystem.GetInstance().GetFacade<Anonymous>(null) as AnonymousUserFacade;
            DeleteAllData();
            flightDAOPGSQL.Add(CreateFlightForTest());

            var flights_list = flightDAOPGSQL.GetAll();
            var f_list = facade.GetFlightsByOriginCountry(flights_list[0].Origin_Country_Id);

            Assert.AreNotEqual(f_list, null);
            Assert.AreEqual(f_list.Count, 1);
        }
    }
}
