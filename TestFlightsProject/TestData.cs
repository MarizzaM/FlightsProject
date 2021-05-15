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




       

    }
}
