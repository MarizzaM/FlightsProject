using FlightsProject;
using FlightsProject.DAO_PGSQL;
using FlightsProject.Facade;
using FlightsProject.POCO;
using Npgsql;
using System;
using System.Collections.Generic;

namespace Runner
{
    class Program
    {
        //static string conn_string = "Host=localhost;Username=postgres;Password=336527981;Database=FlightsProjectDB";
        static string conn_string_test = "Host=localhost;Username=postgres;Password=336527981;Database=FlightsProjectDBTest";
        static bool TestDbConnection()
        {
            try
            {
                using (var my_conn = new NpgsqlConnection(conn_string_test))
                {
                    my_conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // write error to log file
                return false;
            }
        }

        public static void DeleteAllData()
        {
            using (var my_conn = new NpgsqlConnection(conn_string_test))
            {
                my_conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = my_conn;

                //cmd.CommandText = $"DELETE FROM administrators; DELETE FROM airline_companies; DELETE FROM customers; " +
                //    $"DELETE FROM flights; DELETE FROM tickets; DELETE FROM users;";
                cmd.CommandText = $"DELETE FROM administrators; DELETE FROM flights; DELETE FROM airline_companies; DELETE FROM users; ";
                cmd.ExecuteNonQuery();
                Console.WriteLine($"All data has been deleted successfully from tables'");
            }
        }

        public const int AnonymouseFacade_GetFlightById_FlightFound_FLIGHT_ID = 1;
        public const string AnonymouseFacade_GetFlightById_NAME = "EL AL";
        public const int AnonymouseFacade_GetFlightById_VACANCY = 1;
        static void Main(string[] args)
        {
            TestDbConnection();
            //CountryDAOPGSQL countryDAOPGSQL = new CountryDAOPGSQL();

            //Country countryDominicana = new Country
            //{
            //    Name = "Dominicana"
            //};

            //countryDAOPGSQL.Add(countryDominicana);



            ////var countries = countryDAOPGSQL.GetAll();

            ////foreach (var c in countries)
            ////{
            ////    var id = ((dynamic)c).Id;
            ////    var name = ((dynamic)c).Name;

            ////    Console.WriteLine($"{id} {name}");
            ////}

            //Console.WriteLine(countryDAOPGSQL.Get(6));

            //countryDAOPGSQL.Remove(countryDominicana);

            ////countryDAOPGSQL.Update(countryDominicana);

            //var countries = countryDAOPGSQL.GetAll();

            //foreach (var c in countries)
            //{

            //admin.Add(admin1);       //    var id = ((dynamic)c).Id;
            //    var name = ((dynamic)c).Name;

            //    Console.WriteLine($"{id} {name}");
            //}



            //UserDAOPGSQL user = new UserDAOPGSQL();
            //AdminDAOPGSQL admin = new AdminDAOPGSQL();
            //DeleteAllData();
            //User manager2 = new User
            //{
            //    Username = "manager2",
            //    Password = "m112",
            //    Email = "manager2@gmail.com",
            //    User_Role = 3
            //};
            //user.Add(manager2);




            //var u = user.GetByUserName("manager2");

            //long id = u.Id;

            //Console.WriteLine(id);

            //Admin admin1 = new Admin("Mary", "Mill", 1, id);


            UserDAOPGSQL user = new UserDAOPGSQL();
            FlightDAOPGSQL flightDAOPGSQL = new FlightDAOPGSQL();
            AirlineCompanyDAOPGSQL airlineCompanyDAOPGSQL = new AirlineCompanyDAOPGSQL();
            CountryDAOPGSQL countryDAOPGSQL = new CountryDAOPGSQL();

            AnonymousUserFacade facade = FlightsCenterSystem.GetInstance().GetFacade<Admin>(null) as AnonymousUserFacade;
            DeleteAllData();
            User airline1 = new User
            {
                Username = "airline1",
                Password = "a112",
                Email = "airline1@gmail.com",
                User_Role = 2
            };
            user.Add(airline1);

            var u = user.GetByUserName("airline1");
            long id = u.Id;

            AirlineCompany airline = new AirlineCompany
            {
                Name = "AirlineCompany1",
                Country_Id = 1,
                User_Id = (int)id
            };
            airlineCompanyDAOPGSQL.Add(airline);

            var a = airlineCompanyDAOPGSQL.GetAirlineByUserame("airline1");
            Console.WriteLine("aurline: " + a);
            long a_id = a.Id;

            Flight flight = new Flight
            {
                Airline_Company_Id = (long)a_id,
                Origin_Country_Id = 1,
                Destination_Country_Id = 2,
                Departure_Time = new DateTime(2021, 05, 09, 12, 00, 00),
                Landing_Time = new DateTime(2021, 05, 09, 18, 00, 00),
                Tickets_Remaining = 100
            };
            flightDAOPGSQL.Add(flight);

            var f_list = flightDAOPGSQL.GetAll();
            var f = facade.GetFlightsByDestinationCountry(2);

            Console.WriteLine(f[0].Id);
            Console.WriteLine(f[0].Landing_Time);
            Console.WriteLine(f[0].NameOfOriginCountry);


        }
    }
}
