using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.POCO
{
    public class Ticket : IPoco
    {
        public long Id { get; set; }
        public long Id_Flight { get; set; }
        public long Id_Customer { get; set; }

        public Ticket()
        {
        }
        public Ticket(long id_Flight, long id_Customer)
        {
            Id_Flight = id_Flight;
            Id_Customer = id_Customer;
        }
        public static bool operator ==(Ticket t1, Ticket t2)
        {
            if (t1 == null && t2 == null)
                return true;
            if (t1 == null || t2 == null)
                return false;

            return (t1.Id == t2.Id);
        }

        public static bool operator !=(Ticket t1, Ticket t2)
        {
            return !(t1 == t2);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Ticket t = obj as Ticket;
            if (t == null)
                return false;

            return this.Id == t.Id;
        }



        public override string ToString()
        {
            return $"{Id} {Id_Flight} {Id_Customer} ";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
