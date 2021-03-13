using FlightsProject.I_DAO;
using FlightsProject.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.DAO_PGSQL
{
    class CustomerDAOPGSQL : ICustomerDAO
    {
        static string conn_string = "Host=localhost;Username=postgres;Password=336527981;Database=FlightsProjectDB";
        public void Add(Customer t)
        {
            throw new NotImplementedException();
        }

        public Customer Get(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Customer GetGetCustomerByUserName(string username)
        {
            throw new NotImplementedException();
        }

        public void Remove(Customer t)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer t)
        {
            throw new NotImplementedException();
        }
    }
}
