using FlightsProject.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestFlightsProject
{
    class TestData
    {
        //AnonymousUserFacadeTestData

        //CreateAirlineUserForTest
        public static readonly string AnonymouseFacade_CreateAirlineUser_Username = "airline";
        public static readonly string AnonymouseFacade_CreateAirlineUser_Password = "159951";
        public static readonly string AnonymouseFacade_CreateAirlineUser_Email = "airline@gmail.com";
        public static readonly int AnonymouseFacade_CreateAirlineUser_User_Role = 2;

        //CreateAirlineCompanyForTest
        public static readonly string AnonymouseFacade_CreateAirlineCompany_Name = "AirlineCompany";
        public static readonly int AnonymouseFacade_CreateAirlineCompany_Country_Id = 1;

        //CreateFlightForTest
        public static readonly DateTime AnonymouseFacade_CreateFlight_DepartureTime = DateTime.ParseExact("2019-07-08 12:00:00", "yyyy-MM-dd HH:mm:ss", null);
        public static readonly DateTime AnonymouseFacade_CreateFlight_LandingTime = DateTime.ParseExact("2019-07-08 12:00:00", "yyyy-MM-dd HH:mm:ss", null);
        public static readonly int AnonymouseFacade_CreateFlight_TicketsRemaining = 303;




        User manager1 = new User("manager1", "m111", "manager1@gmail.com", 3);



        public static readonly string CreateAdminTest_username = "manager1";
        public static readonly string CreateAdminTest_password = "m111";
        public static readonly string CreateAdminTest_email = "manager1@gmail.com";
        public static readonly int CreateAdminTest_user_role = 3;

        public static readonly string CreateAdminTest_firstName = "Mary";
        public static readonly string CreateAdminTest_lastName = "Mill";
        public static readonly int CreateAdminTest_level = 1;
        public static readonly int CreateAdminTest_userID = 1;

        public const int AnonymouseFacade_GetFlightById_FlightFound_FLIGHT_ID = 1;
        public const string AnonymouseFacade_GetFlightById_NAME = "EL AL";
        public const int AnonymouseFacade_GetFlightById_VACANCY = 1;


        //AdministratorFacade : UserNmae, Password
        public const string Administrator_USER_NAME = "admin";
        public const string Administrator_PASSWORD = "9999";

        //AdministratorFacade: Wrong Password
        public const string Administrator_AdministratorPasswordNotFound_PASSWORD = "8888";

        //AdministratorFacade: Customer Data
        public const string AdministratorFacade_CreateNewCustomer_AddCustomer_FIRST_NAME = "Bar";
        public const string AdministratorFacade_CreateNewCustomer_AddCustomer_LAST_NAME = "BoBi";
        public const string AdministratorFacade_CreateNewCustomer_AddCustomer_USER_NAME = "Bar12345";
        public const string AdministratorFacade_CreateNewCustomer_AddCustomer_PASSWORD = "1234";
        public const string AdministratorFacade_CreateNewCustomer_AddCustomer_ADDRESS = "Or Akiva";
        public const string AdministratorFacade_CreateNewCustomer_AddCustomer_PHONE_NO = "0505468844";
        public const string AdministratorFacade_CreateNewCustomer_AddCustomer_CREDIT_CARD_NUMBER = "37237238";

        //AdministratorFacade: Updated Customer Data
        public const string AdministratorFacade_UpdateCustomerDetail_CustomerUpdated_FIRST_NAME = "Yael";
        public const string AdministratorFacade_UpdateCustomerDetail_CustomerUpdated_LAST_NAME = "Levi";
        public const string AdministratorFacade_UpdateCustomerDetail_CustomerUpdated_USER_NAME = "Yael12345";
        public const string AdministratorFacade_UpdateCustomerDetail_CustomerUpdated_PASSWORD = "12345";
        public const string AdministratorFacade_UpdateCustomerDetail_CustomerUpdated_ADDRESS = "Rehovot";
        public const string AdministratorFacade_UpdateCustomerDetail_CustomerUpdated_PHONE_NO = "0505348857";
        public const string AdministratorFacade_UpdateCustomerDetail_CustomerUpdated_CREDIT_CARD_NUMBER = "37231138";

        //AdministratorFacade: Country Data
        public const string AdministratorFacade_CreateNewCountry_AddCountry_COUNTRY_NAME1 = "Israel";
        public const string AdministratorFacade_CreateNewCountry_AddCountry_COUNTRY_NAME2 = "Russia";

        //AdministratorFacade: AirlineCompany1 Data
        public const string AdministratorFacade_CreateNewAirline1_AddAirlineCompany_AIRLINE_NAME = "El-AL";
        public const string AdministratorFacade_CreateNewAirline1_AddAirlineCompany_USER_NAME = "ELAL12345";
        public const string AdministratorFacade_CreateNewAirline1_AddAirlineCompany_PASSWORD = "12345";

        //AdministratorFacade: AirlineCompany2 Data
        public const string AdministratorFacade_CreateNewAirline2_AddAirlineCompany_AIRLINE_NAME = "Aeroflot";
        public const string AdministratorFacade_CreateNewAirline2_AddAirlineCompany_USER_NAME = "Aeroflot12345";
        public const string AdministratorFacade_CreateNewAirline2_AddAirlineCompany_PASSWORD = "123456";

        //AdministratorFacade: Updated AirlineCompany Data
        public const string AdministratorFacade_UpdateAirlineDetail_AirlineCompanyUpdated_AIRLINE_NAME = "Arkia";
        public const string AdministratorFacade_UpdateAirlineDetail_AirlineCompanyUpdated_USER_NAME = "Arkia12345";
        public const string AdministratorFacade_UpdateAirlineDetail_AirlineCompanyUpdated_PASSWORD = "252525";

        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// 

        //AirlineCompanyFacade: AirlineCompany Data
        public const string AirlineCompanyFacade_AIRLINE_NAME = "El-AL";
        public const string AirlineCompanyFacade_USER_NAME = "ELAL12345";
        public const string AirlineCompanyFacade_PASSWORD = "12345";

        //AirlineCompanyFacade: Wrong Password
        public const string AirlineCompanyFacad_AirlineCompanyPasswordNotFound_PASSWORD = "8888";

        //AirlineCompanyFacade: Updated AirlineCompany Data
        public const string AirlineCompanyFacade_UpdatedName_AIRLINE_NAME = "El-AL TOURS";
        public const string AirlineCompanyFacade_NewPassword_PASSWORD = "ELAL1234567";

        //AirlineCompanyFacade: Flight Data
        public static DateTime AirlineCompanyFacade_CreateNewFlight_DEPARTURE_TIME = DateTime.ParseExact("2019-07-08 12:00:00", "yyyy-MM-dd HH:mm:ss", null);
        public static DateTime AirlineCompanyFacade_CreateNewFlight_LANDING_TIME = DateTime.ParseExact("2019-07-08 18:00:00", "yyyy-MM-dd HH:mm:ss", null);
        public const int AirlineCompanyFacade_CreateNewFlight_REMANING_TICKETS = 100;
        public const int AirlineCompanyFacade_CreateNewFlight_TOTAL_TICKETS = 100;

        //AirlineCompanyFacade: Updated Flight Data
        public static DateTime AirlineCompanyFacade_UpdateFlightDetail_DEPARTURE_TIME = DateTime.ParseExact("2019-07-08 11:00:00", "yyyy-MM-dd HH:mm:ss", null);
        public static DateTime AirlineCompanyFacade_UpdateFlightDetail_LANDING_TIME = DateTime.ParseExact("2019-07-08 17:00:00", "yyyy-MM-dd HH:mm:ss", null);
        public const int AirlineCompanyFacade_UpdateFlightDetail_REMANING_TICKETS = 100;
        public const int AirlineCompanyFacade_UpdateFlightDetail_TOTAL_TICKETS = 100;

        //AirlineCompanyFacade: Updated Flight2 Data
        public static DateTime AirlineCompanyFacade_Flight2Detail_DEPARTURE_TIME = DateTime.ParseExact("2019-07-08 10:00:00", "yyyy-MM-dd HH:mm:ss", null);
        public static DateTime AirlineCompanyFacade_Flight2Detail_LANDING_TIME = DateTime.ParseExact("2019-07-08 17:00:00", "yyyy-MM-dd HH:mm:ss", null);
        public const int AirlineCompanyFacade_Flight2Detail_REMANING_TICKETS = 5;
        public const int AirlineCompanyFacade_Fligh2tDetail_TOTAL_TICKETS = 100;

        //AirlineCompanyFacade: Country1 Data
        public const string AirlineCompanyFacade_CreateNewCountry1_AddCountry_COUNTRY_NAME = "Israel";

        //AirlineCompanyFacade: Country2 Data
        public const string AirlineCompanyFacade_CreateNewCountry2_AddCountry_COUNTRY_NAME = "Russia";

        //AirlineCompanyFacade: Customer1 Data
        public const string AirlineCompanyFacade_Customer1_FIRST_NAME = "Bar";
        public const string AirlineCompanyFacade_Customer1_LAST_NAME = "BoBi";
        public const string AirlineCompanyFacade_Customer1_USER_NAME = "Bar12345";
        public const string AirlineCompanyFacade_Customer1_PASSWORD = "1234";
        public const string AirlineCompanyFacade_Customer1_ADDRESS = "Or Akiva";
        public const string AirlineCompanyFacade_Customer1_PHONE_NO = "0505468844";
        public const string AirlineCompanyFacade_Customer1_CREDIT_CARD_NUMBER = "37237238";

        //AirlineCompanyFacade: Customer2 Data
        public const string AirlineCompanyFacade_Customer2_FIRST_NAME = "Yael";
        public const string AirlineCompanyFacade_Customer2_LAST_NAME = "Levi";
        public const string AirlineCompanyFacade_Customer2_USER_NAME = "Yael12345";
        public const string AirlineCompanyFacade_Customer2_PASSWORD = "12345";
        public const string AirlineCompanyFacade_Customer2_ADDRESS = "Rehovot";
        public const string AirlineCompanyFacade_Customer2_PHONE_NO = "0505348857";
        public const string AirlineCompanyFacade_Customer2_CREDIT_CARD_NUMBER = "37231138";

        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// 

        //CustomerFacade: Customer Data
        public const string CustomerFacade_Customer_FIRST_NAME = "Bar";
        public const string CustomerFacade_Customer_LAST_NAME = "BoBi";
        public const string CustomerFacade_Customer_USER_NAME = "Bar12345";
        public const string CustomerFacade_Customer_PASSWORD = "1234";
        public const string CustomerFacade_Customer_ADDRESS = "Or Akiva";
        public const string CustomerFacade_Customer_PHONE_NO = "0505468844";
        public const string CustomerFacade_Customer_CREDIT_CARD_NUMBER = "37237238";

        //CustomerFacade: Customer Wrong Password
        public const string CustomerFacade_CustomerWrongPassword_PASSWORD = "1234567";

        //CustomerFacade: Flight Data
        public static DateTime CustomerFacade_CreateNewFlight_DEPARTURE_TIME = DateTime.ParseExact("2019-07-08 12:00:00", "yyyy-MM-dd HH:mm:ss", null);
        public static DateTime CustomerFacade_CreateNewFlight_LANDING_TIME = DateTime.ParseExact("2019-07-08 18:00:00", "yyyy-MM-dd HH:mm:ss", null);
        public const int CustomerFacade_CreateNewFlight_REMANING_TICKETS = 100;
        public const int CustomerFacade_CreateNewFlight_TOTAL_TICKETS = 100;

        //CustomerFacade: AirlineCompany1 Data
        public const string CustomerFacade_AirlineCompany1_AIRLINE_NAME = "El-AL";
        public const string CustomerFacade_AirlineCompany1_USER_NAME = "ELAL12345";
        public const string CustomerFacade_AirlineCompany1_PASSWORD = "12345";

        //CustomerFacade: AirlineCompany2 Data
        public const string CustomerFacade_AirlineCompany2_AIRLINE_NAME = "Aeroflot";
        public const string CustomerFacade_AirlineCompany2_USER_NAME = "Aeroflot12345";
        public const string CustomerFacade_AirlineCompany2_PASSWORD = "123456";

        //CustomerFacade:
        public const string CustomerFacade_COUNTRY_NAME2 = "Russia";

        //CustomerFacade: Updated Flight Data
        public static DateTime CustomerFacade_UpdateFlightDetail_DEPARTURE_TIME = DateTime.ParseExact("2019-07-08 11:00:00", "yyyy-MM-dd HH:mm:ss", null);
        public static DateTime CustomerFacade_UpdateFlightDetail_LANDING_TIME = DateTime.ParseExact("2019-07-08 17:00:00", "yyyy-MM-dd HH:mm:ss", null);
        public const int CustomerFacade_UpdateFlightDetail_REMANING_TICKETS = 5;
        public const int CustomerFacade_UpdateFlightDetail_TOTAL_TICKETS = 100;

        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// 

        //AnonymousFacade: Flight Data
        public const int AnonymousFacade_Flight_Id = 1;
        public const int AnonymousFacade_Flight_Airline_Company_Id = 1;
        public const int AnonymousFacade_Flight_Origin_Country_Id = 1;
        public const int AnonymousFacade_Flight_Destination_Country_Id = 2;
        public static DateTime AnonymousFacade_Flight_Departure_Time = DateTime.ParseExact("2019-07-08 12:00:00", "yyyy-MM-dd HH:mm:ss", null);
        public static DateTime AnonymousFacade_Flight_Landing_Time = DateTime.ParseExact("2019-07-08 18:00:00", "yyyy-MM-dd HH:mm:ss", null);
        public const int AnonymousFacade_Flight_Tickets_Remaining = 100;
  

        //AnonymousFacade: Updated Flight Data
        public static DateTime AnonymousFacade_UpdateFlightDetail_DEPARTURE_TIME = DateTime.ParseExact("2019-07-08 11:00:00", "yyyy-MM-dd HH:mm:ss", null);
        public static DateTime AnonymousFacadee_UpdateFlightDetail_LANDING_TIME = DateTime.ParseExact("2019-07-08 17:00:00", "yyyy-MM-dd HH:mm:ss", null);
        public const int AnonymousFacade_UpdateFlightDetail_REMANING_TICKETS = 5;
        public const int AnonymousFacadee_UpdateFlightDetail_TOTAL_TICKETS = 100;

        //AnonymousFacade: AirlineCompany Data1
        public const string AnonymousFacade_CreateNewAirlineCompany1_AIRLINE_NAME = "El-AL";
        public const string AnonymousFacade_CreateNewAirlineCompany1_USER_NAME = "ELAL12345";
        public const string AnonymousFacade_CreateNewAirlineCompany1_PASSWORD = "12345";

        //AnonymousFacade: AirlineCompany Data2
        public const string AnonymousFacade_CreateNewAirlineCompany2_AIRLINE_NAME = "Aeroflot";
        public const string AnonymousFacade_CreateNewAirlineCompany2_USER_NAME = "Aeroflot12345";
        public const string AnonymousFacade_CreateNewAirlineCompan2_PASSWORD = "123456";

        //AnonymousFacade: Customer Data
        public const string AnonymousFacade_Customer1_FIRST_NAME = "Bar";
        public const string AnonymousFacade_Customer1_LAST_NAME = "BoBi";
        public const string AnonymousFacade_Customer1_USER_NAME = "Bar12345";
        public const string AnonymousFacade_Customer1_PASSWORD = "1234";
        public const string AnonymousFacade_Customer1_ADDRESS = "Or Akiva";
        public const string AnonymousFacade_Customer1_PHONE_NO = "0505468844";
        public const string AnonymousFacade_Customer1_CREDIT_CARD_NUMBER = "37237238";

    }
}
