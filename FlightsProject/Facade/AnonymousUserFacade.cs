using FlightsProject.I_Facade;
using FlightsProject.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.Facade
{
    public class AnonymousUserFacade : FacadeBase, IAnonymousUserFacade
    {
        public Country GetCountry(int id)
        {
            return _countryDAO.Get(id);
        }

        public AirlineCompany GetAirlineCompany(int id)
        {
            return _airlineDAO.Get(id);
        }
        public IList<AirlineCompany> GetAllAirlineCompanies()
        {
            return _airlineDAO.GetAll();
        }

        public IList<Flight> GetAllFlights()
        {
            return _flightDAO.GetAll();
        }

        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            return (Dictionary<Flight, int>)_flightDAO.GetAllFlightsVacancy();

        }

        public Flight GetFlightById(int id)
        {
            return _flightDAO.GetFlightsById(id);
        }

        public IList<Flight> GetFlightsByDepatrureDate(DateTime departureDate)
        {
            return _flightDAO.GetFlightsByDepatrureDate(departureDate);
        }

        public IList<Flight> GetFlightsByDestinationCountry(int countryCode)
        {
            return _flightDAO.GetFlightsByDestinationCountry(countryCode);
        }

        public IList<Flight> GetFlightsByLandingDate(DateTime landingDate)
        {
            return _flightDAO.GetFlightsByLandingDate(landingDate);
        }

        public IList<Flight> GetFlightsByOriginCountry(int countryCode)
        {
            return _flightDAO.GetFlightsByOriginCountry(countryCode);
        }
    }
}
