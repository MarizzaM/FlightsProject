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
    public class AirlineController : ControllerBase
    {
        private void AuthenticateAndGetTokenAndGetFacade(out LoginToken<AirlineCompany> tokenAirline,
                                                                                   out LoggedsInAirlineFacade fasadeAirline)
        {
            // after we learned authentication
            // 1. validate token
            // 2. retrieve LoginToken<Customer>
            // 3. get Customer facade

            // before we learn authentication
            // 1. perform login  -- use real user-name + pwd
            // 2. get the token + facade

            ILoginService loginService = new LoginService();
            loginService.TryAirlineLogin("airline99", "LiGpmH", out tokenAirline);
            fasadeAirline = FlightsCenterSystem.GetInstance().GetFacade(tokenAirline) as LoggedsInAirlineFacade;
        }

        // GET: api/<CompanyFacadeController>
        [HttpGet("get_all_tickets")]
        public IList<Ticket> GetAllTickets()
        {
            AuthenticateAndGetTokenAndGetFacade(out LoginToken<AirlineCompany> tokenAirline,
                                                                                   out LoggedsInAirlineFacade fasadeAirline);
            IList<Ticket> result = fasadeAirline.GetAllTickets(tokenAirline);
            return result;
        }


        [HttpPost("create_flight")]
        public async Task<ActionResult> CreateFlight([FromBody] Flight flight)
        {
            AuthenticateAndGetTokenAndGetFacade(out LoginToken<AirlineCompany> tokenAirline,
                                                                      out LoggedsInAirlineFacade facadeAirline);
            try
            {
                await Task.Run(() => facadeAirline.CreateFlight(tokenAirline, flight));
            }
            catch (IllegalFlightParameter ex)
            {
                return StatusCode(400, $"{{ error: \"{ex.Message}\" }}"); // 400 + body = ex.Message
                //return BadRequest($"{{ error: {ex.Message} }}"); // 400 + body = ex.Message
            }
            return null;
        }

        // PUT api/<CompanyFacadeController>/5
        [HttpPut("mofidy_airline_details/{airline_id}")]
        public async Task<ActionResult> MofidyAirlineDetails([FromBody] AirlineCompany airline)
        {
            AuthenticateAndGetTokenAndGetFacade(out LoginToken<AirlineCompany> tokenAirline,
                                                                      out LoggedsInAirlineFacade facadeAirline);
            try
            {
                await Task.Run(() => facadeAirline.MofidyAirlineDetails(tokenAirline, airline));
            }
            catch (IllegalFlightParameter ex)
            {
                return StatusCode(400, $"{{ error: \"{ex.Message}\" }}"); // 400 + body = ex.Message
                //return BadRequest($"{{ error: {ex.Message} }}"); // 400 + body = ex.Message
            }
            return null;
        }

    }
}
