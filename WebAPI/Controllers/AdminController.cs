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
    public class AdminController : ControllerBase
    {
        private void AuthenticateAndGetTokenAndGetFacade(out LoginToken<Admin> tokenAdmin,
                                                                                   out LoggedInAdministratorFacade facadeAdmin)
        {
            // after we learned authentication
            // 1. validate token
            // 2. retrieve LoginToken<Customer>
            // 3. get Customer facade

            // before we learn authentication
            // 1. perform login  -- use real user-name + pwd
            // 2. get the token + facade

            ILoginService loginService = new LoginService();
            loginService.TryAdminLogin("manager87", "lF9A7v", out tokenAdmin);
            facadeAdmin = FlightsCenterSystem.GetInstance().GetFacade(tokenAdmin) as LoggedInAdministratorFacade;
        }

            // GET: api/<AdministratorFacadeApiController>
            [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AdministratorFacadeApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AdministratorFacadeApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AdministratorFacadeApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("removecustomer/{customer}")]
        public void RemoveCustomer(Customer customer)
        {
            AuthenticateAndGetTokenAndGetFacade(out LoginToken<Admin> tokenAdmin,
                                                                      out LoggedInAdministratorFacade fasadeAdmin);
            fasadeAdmin.RemoveCustomer(tokenAdmin, customer);
        }
    }
}
