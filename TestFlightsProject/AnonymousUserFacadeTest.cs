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

                cmd.CommandText = $"DELETE FROM administrators; DELETE FROM flights; DELETE FROM airline_companies; DELETE FROM users;";


                //cmd.CommandText = $"DELETE FROM administrators; DELETE FROM airline_companies; DELETE FROM customers; " +
                //    $"DELETE FROM flights; DELETE FROM tickets; DELETE FROM users;";
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
            User u = userDAOPGSQL.GetByUserName("TestData.AnonymouseFacade_CreateAirlineUser_Username");

            AirlineCompany airlineCompany = new AirlineCompany
            {
                Name = TestData.AnonymouseFacade_CreateAirlineCompany_Name,
                Country_Id = TestData.AnonymouseFacade_CreateAirlineCompany_Country_Id,
                User_Id = u.Id,
            };
            return airlineCompany;
        }

        public Flight CreateFlightForTest()
        {
            airlineCompanyDAOPGSQL.Add(CreateAirlineCompanyForTest());
            AirlineCompany ac = airlineCompanyDAOPGSQL.GetAirlineByUserame("TestData.AnonymouseFacade_CreateAirlineUser_Username");

            Flight flight = new Flight
            {
                Airline_Company_Id = ac.Id,
                Origin_Country_Id = 1,
                Destination_Country_Id = 2,
                Departure_Time = TestData.AnonymouseFacade_CreateFlight_DepartureTime,
                Landing_Time = TestData.AnonymouseFacade_CreateFlight_LandingTime,
                Tickets_Remaining = TestData.AnonymouseFacade_CreateFlight_TicketsRemaining
            };
            return flight;
        }

        [TestMethod]
        public void GetAllAirlineCompaniesTest()
        {
        }
        [TestMethod]
        public void GetAllFlightsTest()
        {
        }
        [TestMethod]
        public void GetAllFlightsVacancyTest()
        {
        }



        [TestMethod]
        public void GetFlightByIdTest()
        {

            AnonymousUserFacade facade = FlightsCenterSystem.GetInstance().GetFacade<Admin>(null) as AnonymousUserFacade;
            DeleteAllData();
            User airline1 = new User
            {
                Username = "airline1",
                Password = "a112",
                Email = "airline1@gmail.com",
                User_Role = 2
            };
            userDAOPGSQL.Add(airline1);

            var u = userDAOPGSQL.GetByUserName("airline1");
            long id = u.Id;

            AirlineCompany airline = new AirlineCompany
            {
                Name = "AirlineCompany1",
                Country_Id = 1,
                User_Id = (int)id
            };
            airlineCompanyDAOPGSQL.Add(airline);

            var a = airlineCompanyDAOPGSQL.GetAirlineByUserame("airline1");
            Console.WriteLine("airline: " + a);
            long a_id = a.Id;

            Flight flight = new Flight
            {
                Airline_Company_Id = (long)a_id,
                Origin_Country_Id = 1,
                Destination_Country_Id = 2,
                Departure_Time = DateTime.ParseExact("2019-07-08 18:00:00", "yyyy-MM-dd HH:mm:ss", null),
                Landing_Time = DateTime.ParseExact("2019-07-08 18:00:00", "yyyy-MM-dd HH:mm:ss", null),
                Tickets_Remaining = 100
            };
            flightDAOPGSQL.Add(flight);
            var flights_list = flightDAOPGSQL.GetAll();
            
            Flight f = facade.GetFlightById((int)flights_list[0].Id);

            Assert.AreNotEqual(f, null);





        }
        [TestMethod]
        public void GetFlightsByDepatrureDateTest()
        {
        }
        [TestMethod]
        public void GetFlightsByDestinationCountryTest()
        {
        }
        [TestMethod]
        public void GetFlightsByLandingDateTest()
        {
        }
        [TestMethod]
        public void GetFlightsByOriginCountryTest()
        {
        }
    }
}
