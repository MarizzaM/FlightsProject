
using FlightsProject.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.I_DAO
{
    public interface IAirlineCompanyDAO : IBasicDb<AirlineCompany>
    {
        public AirlineCompany GetAirlineByUserame(string name);
        public IList<AirlineCompany> GetAllAirlinesByCountry(int countryId);
    }
}
