using FlightsProject.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTO
{
    public class FlightDTO
    {
        public long Id { get; set; }
        public string Airline_Company_Name { get; set; }
        public string Origin_Country_Name { get; set; }
        public string Destination_Country_Name { get; set; }
        public DateTime Departure_Time { get; set; }
        public DateTime Landing_Time { get; set; }
        public int Tickets_Remaining { get; set; }
    }
}
