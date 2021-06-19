using FlightsProject;
using FlightsProject.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTO
{
    public class CustomerUpdatingDTO : Customer
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Address { get; set; }
        public string Phone_No { get; set; }
        public string Credit_Card_No { get; set; }

    }
}
