using FlightsProject.I_DAO;
using FlightsProject.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.DAO_PGSQL
{
    public class FlightDAOPGSQL : IFlightDAO
    {
        static string conn_string = "Host=localhost;Username=postgres;Password=336527981;Database=FlightsProjectDB";
        public void Add(Flight t)
        {
            throw new NotImplementedException();
        }

        public Flight Get(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Flight> GetAll()
        {
            throw new NotImplementedException();
        }

        public IList<Flight> GetAllFlightsVacancy()
        {
            throw new NotImplementedException();
        }

        public Flight GetFlightsByCustomer(string customer)
        {
            throw new NotImplementedException();
        }

        public Flight GetFlightsByDepartureDate(DateTime dt)
        {
            throw new NotImplementedException();
        }

        public Flight GetFlightsByDestinationCountry(string country)
        {
            throw new NotImplementedException();
        }

        public Flight GetFlightsById(int id)
        {
            throw new NotImplementedException();
        }

        public Flight GetFlightsByLandingDate(DateTime dt)
        {
            throw new NotImplementedException();
        }

        public Flight GetFlightsByOriginCountry(string country)
        {
            throw new NotImplementedException();
        }

        public void Remove(Flight t)
        {
            throw new NotImplementedException();
        }

        public void Update(Flight t)
        {
            throw new NotImplementedException();
        }
    }
}
