using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTO
{
    public class TicketDTO
    {
        public long Id_Ticket { get; set; }
        public long Id_Flight { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
    }
}
