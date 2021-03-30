using FlightsProject.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.Login
{
    public interface ILoginService
    {
        bool TryAdminLogin(string username, string password, out LoginToken<Admin> token);
        bool TryAirlineLogin(string username, string password, out LoginToken<AirlineCompany> token);
        bool TryCustomerLogin(string username, string password, out LoginToken<Customer> token);
    }
}
