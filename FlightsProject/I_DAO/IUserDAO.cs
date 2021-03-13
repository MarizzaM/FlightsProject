using FlightsProject.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.I_DAO
{
    public interface IUserDAO : IBasicDb<User>
    {
        public User GetByUserName(string username);

    }
}
