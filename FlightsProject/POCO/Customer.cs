using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.POCO
{
    public class Customer : IPoco, IUser
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Address { get; set; }
        public string Phone_No { get; set; }
        public string Credit_Card_No { get; set; }
        public int User_Id { get; set; }

        public Customer()
        {
        }

        public Customer(string first_Name, string last_Name, string address, string phone_No, string credit_Card_No, int user_Id)
        {
            First_Name = first_Name;
            Last_Name = last_Name;
            Address = address;
            Phone_No = phone_No;
            Credit_Card_No = credit_Card_No;
            User_Id = user_Id;
        }

        public static bool operator ==(Customer c1, Customer c2)
        {
            if (c1 == null && c2 == null)
                return true;
            if (c1 == null || c2 == null)
                return false;

            return (c1.Id == c2.Id);
        }

        public static bool operator !=(Customer c1, Customer c2)
        {
            return !(c1 == c2);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Customer c = obj as Customer;
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
            return $"{Id} {First_Name} {Last_Name} {Address} {Phone_No} {Credit_Card_No} {User_Id} ";
        }
    }
}
