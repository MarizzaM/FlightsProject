using FlightsProject.I_Facade;
using FlightsProject.Login;
using FlightsProject.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.Facade
{
    public class LoggedInCustomerFacade : AnonymousUserFacade, ILoggedInCustomerFacade
    {
        public void CancelTicket(LoginToken<Customer> token, Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public IList<Flight> GetAllMyFlights(LoginToken<Customer> token)
        {
            throw new NotImplementedException();
        }

        public Ticket PurchaseTicket(LoginToken<Customer> token, Flight flight)
        {
            throw new NotImplementedException();
        }
    }
}
