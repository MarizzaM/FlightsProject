using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.POCO
{
    public class AirlineCompany : IPoco, IUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Country_Id { get; set; }
        public int User_Id { get; set; }

        public AirlineCompany()
        {
        }

        public AirlineCompany(string name, int country_Id, int user_Id)
        {
            Name = name;
            Country_Id = country_Id;
            User_Id = user_Id;
        }

        public static bool operator ==(AirlineCompany ac1, AirlineCompany ac2)
        {
            if (ac1 == null && ac2 == null)
                return true;
            if (ac1 == null || ac2 == null)
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
            return this.Id;
        }

        public override string ToString()
        {
            return $"{Id} {Name} {Country_Id} {User_Id}";
        }
    }
}
