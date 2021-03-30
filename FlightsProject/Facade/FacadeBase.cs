using FlightsProject.I_DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.Facade
{
    public abstract class FacadeBase
    {
        protected IAirlineCompanyDAO _airlineDAO;
        protected ICountryDAO _countryDAO;
        protected ICustomerDAO _customerDAO;
        protected IAdminDAO _adminDAO;
        protected IUserDAO _userDAO;
        protected IFlightDAO _flightDAO;
        protected ITicketDAO _ticketDAO;
    }
}
