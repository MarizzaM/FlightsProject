using FlightsProject.I_DAO;
using FlightsProject.POCO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.DAO_PGSQL
{
    public class TicketDAOPGSQL : ITicketDAO
    {
        static string conn_string = "Host=localhost;Username=postgres;Password=336527981;Database=FlightsProjectDB";
        public void Add(Ticket t)
        {
            throw new NotImplementedException();
        }

        public Ticket Get(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Ticket> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(Ticket t)
        {
            throw new NotImplementedException();
        }

        public void Update(Ticket t)
        {
            throw new NotImplementedException();
        }
    }
}
