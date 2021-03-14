using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.POCO
{
    public class Flight : IPoco
    {
        public int Id { get; set; }
        public int Airline_Company_Id { get; set; }
        public int Origin_Country_Id { get; set; }
        public int Destination_Country_Id { get; set; }
        public DateTime Departure_Time { get; set; }
        public DateTime Landing_Time { get; set; }
        public int Tickets_Remaining { get; set; }
        public string NameOfCountry { get; internal set; }
        public string NameOfOriginCountry { get; internal set; }

        public Flight()
        {
        }

        public Flight(int airline_Company_Id, int origin_Country_Id, int destination_Country_Id, DateTime departure_Time, DateTime landing_Time, int tickets_Remaining)
        {
            Airline_Company_Id = airline_Company_Id;
            Origin_Country_Id = origin_Country_Id;
            Destination_Country_Id = destination_Country_Id;
            Departure_Time = departure_Time;
            Landing_Time = landing_Time;
            Tickets_Remaining = tickets_Remaining;
        }

        public static bool operator ==(Flight f1, Flight f2)
        {
            if (f1 == null && f2 == null)
                return true;
            if (f1 == null || f2 == null)
                return false;

            return (f1.Id == f2.Id);
        }

        public static bool operator !=(Flight f1, Flight f2)
        {
            return !(f1 == f2);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Flight f = obj as Flight;
            if (f == null)
                return false;

            return this.Id == f.Id;
        }

        public override int GetHashCode()
        {
            return this.Id;
        }

        public override string ToString()
        {
            return $"{Id} {Airline_Company_Id} {Origin_Country_Id} {Destination_Country_Id} {Departure_Time}" +
                $"{Landing_Time} {Tickets_Remaining}";
        }

    }
}
