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
            [HttpGet("getallcustomers")]
        public async Task<IList<Customer>> GetAllCustomers()
        {
            var facade = new LoggedInAdministratorFacade();
            IList<Customer> result = await Task.Run(() => facade.GetAllCustomers(new LoginToken<Admin>()));
            return result;
        }

        [HttpPost("createadmin")]
        public async Task<ActionResult> CreateAdmin([FromBody] Admin admin)
        {
            AuthenticateAndGetTokenAndGetFacade(out LoginToken<Admin> tokenAdmin,
                                                                      out LoggedInAdministratorFacade fasadeAdmin);
            try
            {
                await Task.Run(() => fasadeAdmin.CreateAdmin(tokenAdmin, admin));
            }
            catch (IllegalFlightParameter ex)
            {
                return StatusCode(400, $"{{ error: \"{ex.Message}\" }}"); 
            }
            return null;
        }

        [HttpPost("createairline")]
        public async Task<ActionResult> CreateNewAirline([FromBody] AirlineCompany airline)
        {
            AuthenticateAndGetTokenAndGetFacade(out LoginToken<Admin> tokenAdmin,
                                                                      out LoggedInAdministratorFacade fasadeAdmin);
            try
            {
                await Task.Run(() => fasadeAdmin.CreateNewAirline(tokenAdmin, airline));
            }
            catch (IllegalFlightParameter ex)
            {
                return StatusCode(400, $"{{ error: \"{ex.Message}\" }}"); 
            }
            return null;
        }

        [HttpPost("createcustomer")]
        public async Task<ActionResult> CreateNewCustomer([FromBody] Customer customer)
        {
            AuthenticateAndGetTokenAndGetFacade(out LoginToken<Admin> tokenAdmin,
                                                                      out LoggedInAdministratorFacade fasadeAdmin);
            try
            {
                await Task.Run(() => fasadeAdmin.CreateNewCustomer(tokenAdmin, customer));
            }
            catch (IllegalFlightParameter ex)
            {
                return StatusCode(400, $"{{ error: \"{ex.Message}\" }}");
            }
            return null;
        }

        [HttpPut("updateadmin/{id}")]
        public async Task<ActionResult> UpdateAdmin(int id, [FromBody] Admin admin)
        {
            AuthenticateAndGetTokenAndGetFacade(out LoginToken<Admin> tokenAdmin,
                                                          out LoggedInAdministratorFacade fasadeAdmin);
            try
            {
                await Task.Run(() => fasadeAdmin.UpdateAdmin(tokenAdmin, admin));
            }
            catch (IllegalFlightParameter ex)
            {
                return StatusCode(400, $"{{ error: \"{ex.Message}\" }}"); 
            }
            return null;
        }

        [HttpPut("updateairline/{id}")]
        public async Task<ActionResult> UpdateAirlineDetails(int id, [FromBody] AirlineCompany airline)
        {
            AuthenticateAndGetTokenAndGetFacade(out LoginToken<Admin> tokenAdmin,
                                                          out LoggedInAdministratorFacade fasadeAdmin);
            try
            {
                await Task.Run(() => fasadeAdmin.UpdateAirlineDetails(tokenAdmin, airline));
            }
            catch (IllegalFlightParameter ex)
            {
                return StatusCode(400, $"{{ error: \"{ex.Message}\" }}");
            }
            return null;
        }

        [HttpPut("updatecustomer/{id}")]
        public async Task<ActionResult> UpdateCustomerDetails(int id, [FromBody] Customer customer)
        {
            AuthenticateAndGetTokenAndGetFacade(out LoginToken<Admin> tokenAdmin,
                                                          out LoggedInAdministratorFacade fasadeAdmin);
            try
            {
                await Task.Run(() => fasadeAdmin.UpdateCustomerDetails(tokenAdmin, customer));
            }
            catch (IllegalFlightParameter ex)
            {
                return StatusCode(400, $"{{ error: \"{ex.Message}\" }}");
            }
            return null;
        }

        [HttpDelete("removecustomer/{customer}")]
        public void RemoveCustomer(Customer customer)
        {
            AuthenticateAndGetTokenAndGetFacade(out LoginToken<Admin> tokenAdmin,
                                                                      out LoggedInAdministratorFacade fasadeAdmin);
            fasadeAdmin.RemoveCustomer(tokenAdmin, customer);
        }

        [HttpDelete("removeadmin/{admin}")]
        public void RemoveAdmin(Admin admin)
        {
            AuthenticateAndGetTokenAndGetFacade(out LoginToken<Admin> tokenAdmin,
                                                                      out LoggedInAdministratorFacade fasadeAdmin);
            fasadeAdmin.RemoveAdmin(tokenAdmin, admin);
        }

        [HttpDelete("removeairline/{airline}")]
        public void RemoveAirline(AirlineCompany airline)
        {
            AuthenticateAndGetTokenAndGetFacade(out LoginToken<Admin> tokenAdmin,
                                                                      out LoggedInAdministratorFacade fasadeAdmin);
            fasadeAdmin.RemoveAirline(tokenAdmin, airline);
        }
    }
}
