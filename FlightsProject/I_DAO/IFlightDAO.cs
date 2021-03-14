using FlightsProject.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.I_DAO
{
    public interface IFlightDAO : IBasicDb<Flight>
    {
        public Dictionary<Flight, int> GetAllFlightsVacancy();
        public IList<Flight> GetFlightsByOriginCountry(int countryCode);
        public IList<Flight> GetFlightsByDestinationCountry(int countryCode);
        public IList<Flight> GetFlightsByDepatrureDate(DateTime departureDate);
        public IList<Flight> GetFlightsByLandingDate(DateTime landingDate);
        public IList<Flight> GetFlightsByCustomer(Customer customer);
        public Flight GetFlightsById(int id);
    }
}
