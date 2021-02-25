using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject
{
    class Admin : IPoco, IUser
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public int Level { get; set; }
        public int User_id { get; set; }

        public Admin()
        {
        }

        public Admin(string first_Name, string last_Name, int level, int user_id)
        {
            First_Name = first_Name;
            Last_Name = last_Name;
            Level = level;
            User_id = user_id;
        }

        public static bool operator ==(Admin a1, Admin a2)
        {
            if (a1 == null && a2 == null)
                return true;
            if (a1 == null || a2 == null)
                return false;

            return (a1.Id == a2.Id);
        }

        public static bool operator !=(Admin a1, Admin a2)
        {
            return !(a1 == a2);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Admin c = obj as Admin;
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
            return $"{Id} {First_Name} {Last_Name} {Level} {User_id}";
        }
    }
}
