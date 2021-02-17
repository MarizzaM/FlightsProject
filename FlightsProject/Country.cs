using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject
{
    class Country : IPoco
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Country()
        {
        }

        public Country(string name)
        {
            Name = name;
        }

        public static bool operator ==(Country c1, Country c2)
        {
            if (c1 == null && c2 == null)
                return true;
            if (c1 == null || c2 == null)
                return false;

            return (c1.Id == c2.Id);
        }

        public static bool operator !=(Country c1, Country c2)
        {
            return !(c1 == c2);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Country c = obj as Country;
            if (c == null)
                return false;

            return this.Id == c.Id;
        }

        public override int GetHashCode()
        {
            return this.Id;
        }

        public override string ToString()
        {
            return $"{Id} {Name}";
        }
    }
}
