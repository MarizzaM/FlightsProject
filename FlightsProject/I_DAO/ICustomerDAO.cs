using FlightsProject.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.I_DAO
{
    public interface ICustomerDAO : IBasicDb<Customer>
    {
        public Customer GetGetCustomerByUserName(string username);

    }
}
