using FlightsProject;
using FlightsProject.Facade;
using FlightsProject.Login;
using FlightsProject.POCO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    [ApiController]
    public class AdminController : FlightControllerBase<Admin>
    {
        private void AuthenticateAndGetTokenAndGetFacade(out LoginToken<Admin> tokenAdmin,
                                                                                   out LoggedInAdministratorFacade facadeAdmin)
        {
            // after we learned authentication
            // 1. validate token
            // 2. retrieve LoginToken<Customer>
            // 3. get Customer facade

            tokenAdmin = GetLoginToken();

            // before we learn authentication
            // 1. perform login  -- use real user-name + pwd
            // 2. get the token + facade

           //ILoginService loginService = new LoginService();
            //loginService.TryAdminLogin("manager87", "lF9A7v", out tokenAdmin);
            facadeAdmin = FlightsCenterSystem.GetInstance().GetFacade(tokenAdmin) as LoggedInAdministratorFacade;
        }
        [HttpGet("val")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("get_all_customers")]
        public async Task<IList<Customer>> GetAllCustomers()
        {
            AuthenticateAndGetTokenAndGetFacade(out LoginToken<Admin> tokenAdmin,
                                                                      out LoggedInAdministratorFacade fasadeAdmin);
            IList<Customer> result = await Task.Run(() => fasadeAdmin.GetAllCustomers(new LoginToken<Admin>()));
            return result;
        }

        [HttpPost("create_admin")]
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

        [HttpPost("create_airline")]
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

        [HttpPost("create_customer")]
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

        [HttpPut("update_admin/{id}")]
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

        [HttpPut("update_airline/{id}")]
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

        [HttpPut("update_customer/{id}")]
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

        [HttpDelete("remove_customer/{customer}")]
        public void RemoveCustomer(Customer customer)
        {
            AuthenticateAndGetTokenAndGetFacade(out LoginToken<Admin> tokenAdmin,
                                                                      out LoggedInAdministratorFacade fasadeAdmin);
            fasadeAdmin.RemoveCustomer(tokenAdmin, customer);
        }

        [HttpDelete("remove_admin/{admin}")]
        public void RemoveAdmin(Admin admin)
        {
            AuthenticateAndGetTokenAndGetFacade(out LoginToken<Admin> tokenAdmin,
                                                                      out LoggedInAdministratorFacade fasadeAdmin);
            fasadeAdmin.RemoveAdmin(tokenAdmin, admin);
        }

        [HttpDelete("remove_airline/{airline}")]
        public void RemoveAirline(AirlineCompany airline)
        {
            AuthenticateAndGetTokenAndGetFacade(out LoginToken<Admin> tokenAdmin,
                                                                      out LoggedInAdministratorFacade fasadeAdmin);
            fasadeAdmin.RemoveAirline(tokenAdmin, airline);
        }
    }
}
