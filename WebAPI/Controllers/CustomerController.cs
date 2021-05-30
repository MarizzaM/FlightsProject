using FlightsProject;
using FlightsProject.Facade;
using FlightsProject.Login;
using FlightsProject.POCO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CustomerController : ControllerBase
    {
        private void AuthenticateAndGetTokenAndGetFacade(out LoginToken<Customer> tokenCustomer,
        out LoggedInCustomerFacade facadeCustomer)
        {
            // after we learned authentication
            // 1. validate token
            // 2. retrieve LoginToken<Customer>
            // 3. get Customer facade

            // before we learn authentication
            // 1. perform login  -- use real user-name + pwd
            // 2. get the token + facade

            ILoginService loginService = new LoginService();
            loginService.TryCustomerLogin("customer89", "NJKlGs", out tokenCustomer);
            facadeCustomer = FlightsCenterSystem.GetInstance().GetFacade(tokenCustomer) as LoggedInCustomerFacade;
        }

        // GET: api/<CustomerFacadeController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CustomerFacadeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CustomerFacadeController>
        [HttpPost("purchase_ticket/{flight}")]
        public void PurchaseTickets([FromBody] Flight flight)
        {
            AuthenticateAndGetTokenAndGetFacade(out LoginToken<Customer> tokenCustomer,
                                                                      out LoggedInCustomerFacade facadeCustomer);
            facadeCustomer.PurchaseTicket(tokenCustomer, flight);
        }

        // PUT api/<CustomerFacadeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerFacadeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
