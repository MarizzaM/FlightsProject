
using FlightsProject.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.I_DAO
{
    public interface IAirlineCompanyDAO : IBasicDb<AirlineCompany>
    {
        public object GetAirlineByUsename(string username);
        public object GetAirlineByCountry(string country);
    }
}
