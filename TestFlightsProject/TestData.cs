using FlightsProject;
using FlightsProject.Login;
using FlightsProject.POCO;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestFlightsProject
{

    class TestData
    {
        static string conn_string_test = "Host=localhost;Username=postgres;Password=336527981;Database=FlightsProjectDBTest";
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

        //CreateAirlineUserForTest
        public static int CreateAirlineUser_Id;
        public static readonly string CreateAirlineUser_Username = "airline";
        public static readonly string CreateAirlineUser_Password = "159951";
        public static readonly string CreateAirlineUser_Email = "airline@gmail.com";
        public static readonly int CreateAirlineUser_UserRole = 2;

        public static readonly string AirlineUser_Password_new = "151151";

        //CreateAirlineCompanyForTest
        public static long CreateAirlineCompany_Id;
        public static readonly string CreateAirlineCompany_Name = "AirlineCompany";
        public static readonly int CreateAirlineCompany_Country_Id = 1;

        //UpdateAirlineCompanyForTest
        public static readonly string UpdateAirlineCompany_Name = "AirlineCompany_new";

        //CreateFlightForTest
        public static readonly long CreateFlight_AirlineCompanyId = CreateAirlineCompany_Id;
        public static readonly int CreateFlight_OriginCountryId = 1;
        public static readonly int CreateFlight_DestinationCountryId = 2;
        public static readonly DateTime CreateFlight_DepartureTime = new DateTime(2021, 05, 09, 12, 00, 00);
        public static readonly DateTime CreateFlight_LandingTime = new DateTime(2021, 05, 09, 18, 00, 00);
        public static readonly int CreateFlight_TicketsRemaining = 303;

        //UpdateFlightForTest
        public static readonly DateTime UpdateFlight_DepartureTime = new DateTime(2022, 05, 09, 12, 00, 00);
        public static readonly DateTime UpdateFlight_LandingTime = new DateTime(2022, 05, 09, 18, 00, 00);
        public static readonly int UpdateFlight_TicketsRemaining = 404;


        //CustomerFacadeTestData
        public static int CreateCustomerUser_Id;
        public static readonly string CreateCustomerUser_Username = "customer";
        public static readonly string CreateCustomerUser_Password = "357753";
        public static readonly string CreateCustomerUser_Email = "customer@gmail.com";
        public static readonly int CreateCustomerUser_UserRole = 1;

        //CreateAdminUserForTest
        public static int CreateAdminUser_Id;
        public static readonly string CreateAdminUser_Username = FlightCenterConfig.ADMIN_NAME;
        public static readonly string CreateAdminUser_Password = FlightCenterConfig.ADMIN_PASSWORD;
        public static readonly string CreateAdminUser_Email = "admin@gmail.com";
        public static readonly int CreateAdminUser_UserRole = 3;

        //CreateAdminForTest
        public static readonly string CreateAdmin_FirstName = "fName";
        public static readonly string CreateAdmin_LastName = "lName";
        public static readonly int CreateAdmin_Level = 2;

        //UpdateAdminForTest
        public static readonly string UpdateAdmin_FirstName = "fName_new";
        public static readonly string UpdateAdmin_LastName = "lName_new";
        public static readonly int UpdateAdmin_Level = 1;

        //UpdateCustomerUserForTest
        public static readonly string UpdateCustomerUser_Username = "customer_new";
        public static readonly string UpdateCustomerUser_Password = "357753_new";
        public static readonly string UpdateCustomerUser_Email = "customer_new@customer.com";

        //CreateCustomerForTest
        public static readonly string CreateCustomer_FirstName = "fNameC";
        public static readonly string CreateCustomer_LastName = "lNameC";
        public static readonly string CreateCustomer_Address = "AddressC";
        public static readonly string CreateCustomer_PhoneNo = "Phone_No";
        public static readonly string CreateCustomer_CreditCardNo = "Credit_Card_No";

        //UpdateCustomerForTest
        public static readonly string UpdateCustomer_FirstName = "fNameC_new";
        public static readonly string UpdateCustomer_LastName = "lNameC_new";
        public static readonly string UpdateCustomer_Address = "AddressC_new";
        public static readonly string UpdateCustomer_PhoneNo = "Phone_No_new";
        public static readonly string UpdateCustomer_CreditCardNo = "Credit_Card_No_new";


    }
}
