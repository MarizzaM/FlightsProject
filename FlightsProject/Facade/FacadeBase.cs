using FlightsProject.DAO_PGSQL;
using FlightsProject.I_DAO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.Facade
{
    public abstract class FacadeBase
    {
        protected IAirlineCompanyDAO _airlineDAO = new AirlineCompanyDAOPGSQL();
        protected ICountryDAO _countryDAO = new CountryDAOPGSQL();
        protected ICustomerDAO _customerDAO = new CustomerDAOPGSQL();
        protected IAdminDAO _adminDAO = new AdminDAOPGSQL();
        protected IUserDAO _userDAO = new UserDAOPGSQL();
        protected IFlightDAO _flightDAO = new FlightDAOPGSQL();
        protected ITicketDAO _ticketDAO = new TicketDAOPGSQL();
    }
}
