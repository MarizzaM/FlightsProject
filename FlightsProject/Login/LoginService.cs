using FlightsProject.DAO_PGSQL;
using FlightsProject.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.Login
{
    public class LoginService : ILoginService
    {
        private AirlineCompanyDAOPGSQL _airlineCompanyDAO = new AirlineCompanyDAOPGSQL();
        private CustomerDAOPGSQL _customerDAO = new CustomerDAOPGSQL();

        //public ILoginToken TryLogin(string username, string password) {
        //    LoginToken<Admin> adminToken = new LoginToken<Admin>();
        //    LoginToken<AirlineCompany> airlineToken = new LoginToken<AirlineCompany>();
        //    LoginToken<Customer> customerToken = new LoginToken<Customer>();

        //    if (TryAdminLogin(username, password, out adminToken))
        //        return adminToken;
        //    if (TryAirlineLogin(username, password, out airlineToken))
        //        return airlineToken;
        //    if (TryCustomerLogin(username, password, out customerToken))
        //        return customerToken;
        //    return null;
        //}

        public bool TryAdminLogin(string username, string password, out LoginToken<Admin> token)
        {
            if (username == FlightCenterConfig.ADMIN_NAME && password == FlightCenterConfig.ADMIN_PASSWORD)
            {
                token = new LoginToken<Admin>();
                token.User = new Admin();
                token.User.UserName = FlightCenterConfig.ADMIN_NAME;
                token.User.Password = FlightCenterConfig.ADMIN_PASSWORD;
                return true;
            }

            Admin admin = new Admin();
            if (admin != null)
            {
                if (admin.Password == password)
                {
                    token = new LoginToken<Admin>() { User = admin };
                    return true;
                }
                throw new WrongCredentialsException();
                // wrong passowrd exception
                // catch
            }

            token = null;
            return false;
        }

        public bool TryAirlineLogin(string username, string password, out LoginToken<AirlineCompany> token)
        {
            AirlineCompany company = _airlineCompanyDAO.GetAirlineByUserame(username);
            if (company != null)
            {
                if (company.Password == password)
                {
                    token = new LoginToken<AirlineCompany>() { User = company };
                    return true;
                }
                throw new WrongCredentialsException();
                // wrong passowrd exception
                // catch
            }
            token = null;
            return false;
        }

        public bool TryCustomerLogin(string username, string password, out LoginToken<Customer> token)
        {
            Customer customer = _customerDAO.GetCustomerByUserName(username);
            if (customer != null)
            {
                if (customer.Password == password)
                {
                    token = new LoginToken<Customer>() { User = customer };
                    return true;
                }
                throw new WrongCredentialsException();
                // wrong passowrd exception
                // catch
            }
            token = null;
            return false;
        }
    }
}
