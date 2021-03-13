using FlightsProject.I_DAO;
using FlightsProject.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.DAO_PGSQL
{
    public class UserDAOPGSQL : IUserDAO
    {
        static string conn_string = "Host=localhost;Username=postgres;Password=336527981;Database=FlightsProjectDB";
        public void Add(User t)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public IList<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetByUserName(string username)
        {
            throw new NotImplementedException();
        }

        public void Remove(User t)
        {
            throw new NotImplementedException();
        }

        public void Update(User t)
        {
            throw new NotImplementedException();
        }
    }
}
