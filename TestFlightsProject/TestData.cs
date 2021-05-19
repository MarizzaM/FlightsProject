using FlightsProject;
using FlightsProject.Login;
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
        public static int AnonymouseFacade_CreateAirlineUser_Id;
        public static readonly string AnonymouseFacade_CreateAirlineUser_Username = "airline";
        public static readonly string AnonymouseFacade_CreateAirlineUser_Password = "159951";
        public static readonly string AnonymouseFacade_CreateAirlineUser_Email = "airline@gmail.com";
        public static readonly int AnonymouseFacade_CreateAirlineUser_User_Role = 2;

        //CreateAirlineCompanyForTest
        public static long AnonymouseFacade_CreateAirlineCompany_Id;
        public static readonly string AnonymouseFacade_CreateAirlineCompany_Name = "AirlineCompany";
        public static readonly int AnonymouseFacade_CreateAirlineCompany_Country_Id = 1;

        //CreateFlightForTest
        public static int AnonymouseFacade_CreateFlight_Id;
        public static readonly long AnonymouseFacade_CreateFlight_AirlineCompanyId = AnonymouseFacade_CreateAirlineCompany_Id;
        public static readonly int AnonymouseFacade_CreateFlight_OriginCountryId = 1;
        public static readonly int AnonymouseFacade_CreateFlight_DestinationCountryId = 2;
        public static readonly DateTime AnonymouseFacade_CreateFlight_DepartureTime = new DateTime(2021, 05, 09, 12, 00, 00);
        public static readonly DateTime AnonymouseFacade_CreateFlight_LandingTime = new DateTime(2021, 05, 09, 18, 00, 00);
        public static readonly int AnonymouseFacade_CreateFlight_TicketsRemaining = 303;


        //LoggedInCustomerFacadeTestData

        public static readonly int LoggedInCustomerFacade_CreateCustomerUser_Id;
        public static readonly string LoggedInCustomerFacade_CreateCustomerUser_Username = "customer";
        public static readonly string LoggedInCustomerFacade_CreateCustomerUser_Password = "357753";
        public static readonly string LoggedInCustomerFacade_CreateCustomerUser_Email = "customer@gmail.com";
        public static readonly int LoggedInCustomerFacade_CreateCustomerUser_User_Role = 1;

        //AdminrFacadeTestData

        //CreateAdminUserForTest
        public static int AdminFacade_CreateAdminUser_Id;
        public static readonly string AdminFacade_CreateAdminUser_Username = FlightCenterConfig.ADMIN_NAME;
        public static readonly string AdminFacade_CreateAdminUser_Password = FlightCenterConfig.ADMIN_PASSWORD;
        public static readonly string AdminFacade_CreateAdminUser_Email = "admin@gmail.com";
        public static readonly int AdminFacade_CreateAdminUser_UserRole = 3;

        //CreateAdminForTest
        public static int AdminFacade_CreateAdmin_Id;
        public static readonly string AdminFacade_CreateAdmin_FirstName = "fName";
        public static readonly string AdminFacade_CreateAdmin_LastName = "lName";
        public static readonly int AdminFacade_CreateAdmin_Level = 2;

        //CreateCustomerUserForTest
        public static int AdminFacade_CreateCustomerUser_Id;
        public static readonly string AdminFacade_CreateCustomerUser_Username = "customer";
        public static readonly string AdminFacade_CreateCustomerUser_Password = "357753";
        public static readonly string AdminFacade_CreateCustomerUser_Email = "admin@customer.com";
        public static readonly int AdminFacade_CreateCustomerUser_UserRole = 1;

        //CreateCustomerForTest
        public static int AdminFacade_CreateCustomer_Id;
        public static readonly string AdminFacade_CreateCustomer_FirstName = "fNameC";
        public static readonly string AdminFacade_CreateCustomer_LastName = "lNameC";
        public static readonly string AdminFacade_CreateCustomer_Address = "AddressC";
        public static readonly string AdminFacade_CreateCustomer_PhoneNo = "Phone_No";
        public static readonly string AdminFacade_CreateCustomer_CreditCardNo = "Credit_Card_No";
      

    }
}
