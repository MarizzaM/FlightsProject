using FlightsProject.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.I_DAO
{
    public interface IFlightDAO : IBasicDb<Flight>
    {
        public IList<Flight> GetAllFlightsVacancy();
        public Flight GetFlightsById(int id);
        public Flight GetFlightsByCustomer(string customer);
        public Flight GetFlightsByDepartureDate(DateTime dt);
        public Flight GetFlightsByDestinationCountry(string country);
        public Flight GetFlightsByLandingDate(DateTime dt);
        public Flight GetFlightsByOriginCountry(string country);
    }
}
