using FlightsProject.DAO_PGSQL;
using FlightsProject.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.Login
{
    public class LoginService : ILoginService
    {
        private AirlineCompanyDAOPGSQL _airlineCompanyDAO;
        private CustomerDAOPGSQL _customerDAO;
        private AdminDAOPGSQL _adminDAO;

        public LoginService()
        {
            _airlineCompanyDAO = new AirlineCompanyDAOPGSQL();
        }
        public bool TryAdminLogin(string username, string password, out LoginToken<Admin> token)
        {
            if (username.ToUpper() == FlightCenterConfig.ADMIN_NAME && password == FlightCenterConfig.ADMIN_PASSWORD)
            {
                token = new LoginToken<Admin>();
                token.User = new Admin();
                return true;
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
    
                // wrong passowrd exception
                // catch
            }
            token = null;
            return false;
        }

        public bool TryCustomerLogin(string username, string password, out LoginToken<Customer> token)
        {
            Customer customer = _customerDAO.GetGetCustomerByUserName(username);
            if (customer != null)
            {
                if (customer.Password == password)
                {
                    token = new LoginToken<Customer>() { User = customer };
                    return true;
                }

                // wrong passowrd exception
                // catch
            }
            token = null;
            return false;
        }
    }
}
