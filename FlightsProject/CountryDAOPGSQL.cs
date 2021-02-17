using System;
using System.Collections.Generic;


namespace FlightsProject
{
    public class CountryDAOPGSQL : ICountryDAO
    {
        void IBasicDb<Country>.Add(Country country)
        {
            throw new NotImplementedException();
        }

        Country IBasicDb<Country>.Get(int id)
        {
            throw new NotImplementedException();
        }

        IList<Country> IBasicDb<Country>.GetAll()
        {
            throw new NotImplementedException();
        }

        void IBasicDb<Country>.Remove(Country country)
        {
            throw new NotImplementedException();
        }

        void IBasicDb<Country>.Update(Country country)
        {
            throw new NotImplementedException();
        }
    }
}
