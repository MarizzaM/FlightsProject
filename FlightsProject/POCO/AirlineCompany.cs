using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.POCO
{
    public class AirlineCompany : IPoco, IUser
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Country_Id { get; set; }
        public long User_Id { get; set; }
        public string NameOfAirline { get; internal set; }
        public string NameOfCountry { get; internal set; }
        public string Password { get; set; }
        public string Username { get; set; }

        public AirlineCompany()
        {
        }

        public AirlineCompany(string name, int country_Id, long user_Id)
        {
            Name = name;
            Country_Id = country_Id;
            User_Id = user_Id;
        }

        public static bool operator ==(AirlineCompany ac1, AirlineCompany ac2)
        {
            if (ReferenceEquals(ac1, null) && ReferenceEquals(ac2, null))
                return true;
            if (ReferenceEquals(ac1, null) || ReferenceEquals(ac2, null))
                return false;

            return (ac1.Id == ac2.Id);
        }

        public static bool operator !=(AirlineCompany ac1, AirlineCompany ac2)
        {
            return !(ac1 == ac2);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            AirlineCompany ac = obj as AirlineCompany;
            if (ac == null)
                return false;

            return this.Id == ac.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public override string ToString()
        {
            return $"{Id} {Name} {Country_Id} {User_Id}";
        }


    }
}
