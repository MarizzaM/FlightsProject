using FlightsProject.I_Facade;
using FlightsProject.Login;
using FlightsProject.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.Facade
{
    public class LoggedInAdministratorFacade : AnonymousUserFacade, ILoggedInAdministrator
    {
        public void CreateAdmin(LoginToken<Admin> token, Admin admin)
        {
            throw new NotImplementedException();
        }

        public void CreateNewAirline(LoginToken<Admin> token, AirlineCompany airline)
        {
            throw new NotImplementedException();
        }

        public void CreateNewCustomer(LoginToken<Admin> token, Customer customer)
        {
            throw new NotImplementedException();
        }

        public IList<Customer> GetAllCustomers(LoginToken<Admin> token)
        {
            throw new NotImplementedException();
        }

        public void RemoveAdmin(LoginToken<Admin> token, Admin admin)
        {
            throw new NotImplementedException();
        }

        public void RemoveAirline(LoginToken<Admin> token, AirlineCompany airline)
        {
            throw new NotImplementedException();
        }

        public void RemoveCustomer(LoginToken<Admin> token, Customer customer)
        {
            throw new NotImplementedException();
        }

        public void UpdateAdmin(LoginToken<Admin> token, Admin admin)
        {
            throw new NotImplementedException();
        }

        public void UpdateAirlineDetails(LoginToken<Admin> token, AirlineCompany customer)
        {
            throw new NotImplementedException();
        }

        public void UpdateCustomerDetails(LoginToken<Admin> token, Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
