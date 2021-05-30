using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.POCO
{
    public class Anonymous : IUser
    {
        public string Username { get ; set ; }
        public string Password { get ; set ; }
    }
}
