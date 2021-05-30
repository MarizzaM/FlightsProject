using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject
{
    public interface IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
