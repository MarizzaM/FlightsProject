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
            Flight flight = new Flight();
            if (token != null && ticket.Id_Customer == token.User.Id) {
                _customerDAO.Remove(token.User);
                
                if (ticket.Id_Flight == flight.Id) {
                    flight.Tickets_Remaining++;
                }
            }
        }

        public IList<Flight> GetAllMyFlights(LoginToken<Customer> token)
        {
            if (token != null) {
                return _flightDAO.GetFlightsByCustomer(token.User);
            }
            else
                throw new Exception("Provided username or password is incorrect");
        }

        public Ticket PurchaseTicket(LoginToken<Customer> token, Flight flight)
        {
            
            if (token != null) {
                Ticket ticket = new Ticket(flight.Id, token.User.Id);
                _ticketDAO.Add(ticket);
                return ticket;
            }
            else
                throw new Exception("Provided username or password is incorrect");
        }
    }
}
