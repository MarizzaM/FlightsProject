using FlightsProject.Login;
using FlightsProject.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.I_Facade
{
    public interface ILoggedInAdministrator
    {
        IList<Customer> GetAllCustomers(LoginToken<Admin> token);
        void CreateNewAirline(LoginToken<Admin> token, AirlineCompany airline);
        void UpdateAirlineDetails(LoginToken<Admin> token, AirlineCompany customer);
        void RemoveAirline(LoginToken<Admin> token, AirlineCompany airline);
        void CreateNewCustomer(LoginToken<Admin> token, Customer customer);
        void UpdateCustomerDetails(LoginToken<Admin> token, Customer customer);
        void RemoveCustomer(LoginToken<Admin> token, Customer customer);
        void CreateAdmin(LoginToken<Admin> token, Admin admin);
        void UpdateAdmin(LoginToken<Admin> token, Admin admin);
        void RemoveAdmin(LoginToken<Admin> token, Admin admin);
    }
}
