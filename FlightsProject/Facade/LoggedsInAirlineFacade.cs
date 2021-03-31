using FlightsProject.I_Facade;
using FlightsProject.Login;
using FlightsProject.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.Facade
{
    public class LoggedsInAirlineFacade : AnonymousUserFacade, ILoggedInAirlineFacade
    {
        public void CancelFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (token != null && token.User.Id == flight.Airline_Company_Id) {
                _flightDAO.Remove(flight);
            }
        }

        public void ChangeMyPassword(LoginToken<AirlineCompany> token, string oldPassword, string newPassword)
        {
            if (token != null && token.User.Password == oldPassword ) {
                token.User.Password = newPassword;
            }
        }

        public void CreateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (token != null && flight.Equals(null)) {
                _flightDAO.Add(flight);
            }
        }

        public IList<Flight> GetAllFlights(LoginToken<AirlineCompany> token)
        {
            if (token != null) {
                return _flightDAO.GetAll();
            }
            return null;
        }

        public IList<Ticket> GetAllTickets(LoginToken<AirlineCompany> token)
        {
            if (token != null)
            {
                return _ticketDAO.GetAll();
            }
            return null;
        }

        public void MofidyAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airline)
        {
            if (token != null && token.User.Id == airline.Id) {
                _airlineDAO.Update(airline);
            }
        }

        public void UpdateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (token != null && token.User.Id == flight.Airline_Company_Id)
            {
                _flightDAO.Update(flight);
            }
        }
    }
}
