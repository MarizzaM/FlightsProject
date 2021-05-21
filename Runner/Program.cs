using FlightsProject;
using FlightsProject.DAO_PGSQL;
using FlightsProject.Facade;
using FlightsProject.Login;
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
                cmd.CommandText = $"DELETE FROM administrators; " +
                    $"DELETE FROM tickets; " +
                    $"DELETE FROM flights; " +
                    $"DELETE FROM airline_companies; " +
                    $"DELETE FROM customers; " +
                    $"DELETE FROM users; ";
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
            AdminDAOPGSQL admin = new AdminDAOPGSQL();
            CustomerDAOPGSQL customerDAOPGSQL = new CustomerDAOPGSQL();
            FlightDAOPGSQL flightDAOPGSQL = new FlightDAOPGSQL();
            AirlineCompanyDAOPGSQL airlineCompanyDAOPGSQL = new AirlineCompanyDAOPGSQL();
            CountryDAOPGSQL countryDAOPGSQL = new CountryDAOPGSQL();

            AnonymousUserFacade facade = FlightsCenterSystem.GetInstance().GetFacade<Anonymous>(null) as AnonymousUserFacade;
            DeleteAllData();

            ILoginService loginService = new LoginService();

            //LoginToken<Admin> token = new LoginToken<Admin>(


            //    );
            //token.User.UserName = FlightCenterConfig.ADMIN_NAME;
            //token.User.Password = FlightCenterConfig.ADMIN_PASSWORD;

            User user0 = new User
            {
                Username = "Airline",
                Password = "Airline111",
                Email = "Airline@gmail.com",
                User_Role = 1
            };
            user.Add(user0);

            var uA = user.GetByUserName("Airline");
            long idA = uA.Id;


            AirlineCompany airline = new AirlineCompany
            {
                Name = "AirlineCompany1",
                Country_Id = 1,
                User_Id = (int)idA
            };
            airlineCompanyDAOPGSQL.Add(airline);

            var a = airlineCompanyDAOPGSQL.GetAirlineByUserame("Airline");
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

            User user1 = new User
            {
                Username = "Customer",
                Password = "c112",
                Email = "Customer@gmail.com",
                User_Role = 1
            };
            user.Add(user1);

            var u = user.GetByUserName("Customer");
            long id = u.Id;

            Customer customer = new Customer()
            {
                First_Name = "name",
                Last_Name = "lNAme",
                Address = "Adress",
                Phone_No = "Phone",
                Credit_Card_No = "Card",
                User_Id = id
            };

            loginService.TryAdminLogin(FlightCenterConfig.ADMIN_NAME, FlightCenterConfig.ADMIN_PASSWORD, out LoginToken<Admin> tokenAdmin);
            LoggedInAdministratorFacade facadeAdmin = FlightsCenterSystem.GetInstance().GetFacade<Admin>(tokenAdmin) as LoggedInAdministratorFacade;

            facadeAdmin.CreateNewCustomer(tokenAdmin, customer);
            Customer c = facadeAdmin.GetAllCustomers(tokenAdmin)[0];
            // facadeAdmin.CreateNewCustomer(tokenAdmin, customer);

            
            LoginToken<Customer> tokenCustomer = new LoginToken<Customer>()
            {
                User = c
            };
            //loginService.TryCustomerLogin("Customer", "c112", out tokenCustomer);
            //Customer customer1 = customerDAOPGSQL.GetCustomerByUserName("Customer");
            // loginService.TryCustomerLogin ("Customer", "c112", out tokenCustomer);


            LoggedInCustomerFacade fasadeCustomer = FlightsCenterSystem.GetInstance().GetFacade(tokenCustomer) as LoggedInCustomerFacade;
            var myFlight = flightDAOPGSQL.GetAll()[0];
            Ticket t = fasadeCustomer.PurchaseTicket(tokenCustomer, myFlight);
           // Console.WriteLine(tokenCustomer);
           // Console.WriteLine(customer1.Password);
           // Console.WriteLine(c.U);


            // User manager2 = new User
            // {
            //     Username = "manager2",
            //     Password = "m112",
            //     Email = "manager2@gmail.com",
            //     User_Role = 3
            // };


            // //User manager3 = new User
            // //{
            // //    Username = "manager3",
            // //    Password = "m1122",
            // //    Email = "manager23@gmail.com",
            // //    User_Role = 3
            // //};

            // user.Add(manager2);
            // //user.Add(manager3);


            // var u = user.GetByUserName("manager2");

            //// var u3 = user.GetByUserName("manager3");

            // long id = u.Id;
            // Admin admin1 = new Admin
            // {
            //     First_Name = "Mary",
            //     Last_Name = "Mill",
            //     Level = 3,
            //     User_id = id
            // };



            // //long id3 = u3.Id;
            // //Admin adminLevel1 = new Admin
            // //{
            // //    First_Name = "Parry",
            // //    Last_Name = "Mill",
            // //    Level = 1,
            // //    User_id = id3
            // //};



            //  admin.Add(admin1);


            //var list = admin.GetAll();
            //list.RemoveAt(0);
            //facadeAdminLevel3.RemoveAdmin(tokenAdminLevel3, list[0]);
            //list = admin.GetAll();

            //list.Remove(admin1);
            //list = admin.GetAll();
            //Console.WriteLine(list.Count);

            // Console.WriteLine(token.User.UserName);


            //customerDAOPGSQL.Add(customer);
            //facadeAdmin.CreateNewCustomer(token, customer);
            //var list = facadeAdmin.GetAllCustomers(token);

            //Console.WriteLine(list.Count);





            // var l_cust = facadeAdmin.GetAllCustomers(token);

            //Console.WriteLine(l_cust[1]);

            //AirlineCompany airline = new AirlineCompany
            //{
            //    Name = "AirlineCompany1",
            //    Country_Id = 1,
            //    User_Id = (int)id
            //};
            //airlineCompanyDAOPGSQL.Add(airline);

            //var a = airlineCompanyDAOPGSQL.GetAirlineByUserame("airline1");
            //Console.WriteLine("aurline: " + a);
            //long a_id = a.Id;

            //Flight flight = new Flight
            //{
            //    Airline_Company_Id = (long)a_id,
            //    Origin_Country_Id = 1,
            //    Destination_Country_Id = 2,
            //    Departure_Time = new DateTime(2021, 05, 09, 12, 00, 00),
            //    Landing_Time = new DateTime(2021, 05, 09, 18, 00, 00),
            //    Tickets_Remaining = 100
            //};
            //flightDAOPGSQL.Add(flight);

            //var f_list = flightDAOPGSQL.GetAll();
            //var f = facade.GetFlightsByDestinationCountry(2);

            //Console.WriteLine(f[0].Id);
            //Console.WriteLine(f[0].Landing_Time);
            //Console.WriteLine(f[0].NameOfOriginCountry);
 




        }
    }
}
