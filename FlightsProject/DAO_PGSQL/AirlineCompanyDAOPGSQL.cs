using FlightsProject.I_DAO;
using FlightsProject.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.DAO_PGSQL
{
    class AirlineCompanyDAOPGSQL : IAirlineCompanyDAO
    {
        static string conn_string = "Host=localhost;Username=postgres;Password=336527981;Database=FlightsProjectDB";
        public void Add(AirlineCompany t)
        {
            throw new NotImplementedException();
        }

        public AirlineCompany Get(int id)
        {
            throw new NotImplementedException();
        }

        public AirlineCompany GetAirlineByCountry(int id)
        {
            throw new NotImplementedException();
        }

        public AirlineCompany GetAirlineByCountry(string country)
        {
            throw new NotImplementedException();
        }

        public AirlineCompany GetAirlineByUsename(int id)
        {
            throw new NotImplementedException();
        }

        public AirlineCompany GetAirlineByUsename(string username)
        {
            throw new NotImplementedException();
        }

        public IList<AirlineCompany> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(AirlineCompany t)
        {
            throw new NotImplementedException();
        }

        public void Update(AirlineCompany t)
        {
            throw new NotImplementedException();
        }
    }
}
