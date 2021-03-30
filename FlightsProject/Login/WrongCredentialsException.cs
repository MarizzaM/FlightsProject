using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.Login
{
    public class WrongCredentialsException : Exception
    {
        public WrongCredentialsException()
        {
            Console.WriteLine("Provided username or password is incorrect");
        }
    }
}
