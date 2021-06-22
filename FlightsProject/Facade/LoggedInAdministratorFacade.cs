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
            if (token != null && token.User.Username == FlightCenterConfig.ADMIN_NAME && token.User.Password == FlightCenterConfig.ADMIN_PASSWORD) {

                    _adminDAO.Add(admin);

            }
        }

        public void CreateNewAirline(LoginToken<Admin> token, AirlineCompany airline)
        {
            if (token != null && token.User.Username == FlightCenterConfig.ADMIN_NAME && token.User.Password == FlightCenterConfig.ADMIN_PASSWORD)
            {
                    _airlineDAO.Add(airline);
            }
        }

        public void CreateNewCustomer(LoginToken<Admin> token, Customer customer)
        {
            if (token != null && token.User.Username == FlightCenterConfig.ADMIN_NAME && token.User.Password == FlightCenterConfig.ADMIN_PASSWORD)
            {

                    _customerDAO.Add(customer);

            }
        }

        public IList<Customer> GetAllCustomers(LoginToken<Admin> token)
        {
            if (token != null) {
                return _customerDAO.GetAll();
            }
            return null;
        }

        public void RemoveAdmin(LoginToken<Admin> token, Admin admin)
        {
            if (token.User.Level > admin.Level && token.User.Level == 3 ||
                token.User.Username == FlightCenterConfig.ADMIN_NAME && token.User.Password == FlightCenterConfig.ADMIN_PASSWORD) {
                _adminDAO.Remove(admin);
            }
        }

        public void RemoveAirline(LoginToken<Admin> token, AirlineCompany airline)
        {
            if (token != null && token.User.Level >= 2 ||
                token != null && token.User.Username == FlightCenterConfig.ADMIN_NAME && token.User.Password == FlightCenterConfig.ADMIN_PASSWORD)
            {
                _airlineDAO.Remove(airline);
            }
        }

        public void RemoveCustomer(LoginToken<Admin> token, Customer customer)
        {
            if (token != null && token.User.Level >= 2 ||
                token != null && token.User.Username == FlightCenterConfig.ADMIN_NAME && token.User.Password == FlightCenterConfig.ADMIN_PASSWORD)
            {
                _customerDAO.Remove(customer);
            }
        }

        public void UpdateAdmin(LoginToken<Admin> token, Admin admin)
        {
            if (token != null && token.User.Level > admin.Level && token.User.Level == 3 ||
                token != null && token.User.Username == FlightCenterConfig.ADMIN_NAME && token.User.Password == FlightCenterConfig.ADMIN_PASSWORD)
            {
                _adminDAO.Update(admin);
            }
        }

        public void UpdateAirlineDetails(LoginToken<Admin> token, AirlineCompany airline)
        {
            if (token != null)
            {
                _airlineDAO.Update(airline);
            }
        }

        public void UpdateCustomerDetails(LoginToken<Admin> token, Customer customer)
        {
            if (token != null)
            {
                 _customerDAO.Update(customer);
            }
        }

        // for mapper
        public Customer GetCustomer( int id)
        {
            return _customerDAO.Get(id);
        }
    }
}
