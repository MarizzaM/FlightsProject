using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.POCO
{
    public class User : IPoco
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int User_Role { get; set; }

        public User()
        {
        }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public User(string username, string password, string email, int user_Role)
        {
            Username = username;
            Password = password;
            Email = email;
            User_Role = user_Role;
        }
        public static bool operator ==(User u1, User u2)
        {
            if (ReferenceEquals(u1, null) && ReferenceEquals(u2, null))
                return true;
            if (ReferenceEquals(u1, null) || ReferenceEquals(u2, null))
                return false;

            return (u1.Id == u2.Id);
        }

        public static bool operator !=(User u1, User u2)
        {
            return !(u1 == u2);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            User u = obj as User;
            if (u == null)
                return false;

            return this.Id == u.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public override string ToString()
        {
            return $"{Id} {Username} {Password} {Email} {User_Role}";
        }
    }
}
