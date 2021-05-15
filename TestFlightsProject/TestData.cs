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

    }
}
