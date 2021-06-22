using AutoMapper;
using FlightsProject;
using FlightsProject.Facade;
using FlightsProject.Login;
using FlightsProject.Mappers;
using FlightsProject.POCO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTO;
using WebAPI.Mappers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
   // [Authorize(Roles = "AirlineCompany")]
    [ApiController]
    public class AirlineController : FlightControllerBase<AirlineCompany>
    {
        internal void AuthenticateAndGetTokenAndGetFacade(out LoginToken<AirlineCompany> tokenAirline,
                                                                                   out LoggedsInAirlineFacade fasadeAirline)
        {
            // after we learned authentication
            // 1. validate token
            // 2. retrieve LoginToken<Customer>
            // 3. get Customer facade
           tokenAirline = GetLoginToken();

            // before we learn authentication
            // 1. perform login  -- use real user-name + pwd
            // 2. get the token + facade

            //ILoginService loginService = new LoginService();
           // loginService.TryAirlineLogin("airline99", "LiGpmH", out tokenAirline);
            fasadeAirline = FlightsCenterSystem.GetInstance().GetFacade(tokenAirline) as LoggedsInAirlineFacade;
        }
        //************************************************************
        //******************** GET: api/get_all_tickets ********************
        //************************************************************
        /// <summary>
        /// Get all tickets
        /// </summary>
        /// <response code="200">The request was fulfilled</response>
        /// <response code="204">Server has received the request but there is no information to send back</response>
        /// <response code="400">The request had bad syntax or was inherently impossible to be satisfied</response> 
        /// <response code="401">The request has not been applied because it lacks valid authentication credentials for the target resource</response> 
        /// <response code="404">The server has not found anything matching the URI given</response> 
        /// <response code="500">The server encountered an unexpected condition which prevented it from fulfilling the request</response> 
        /// <response code="501">The server does not support the facility required</response> 
        /// 
        [HttpGet("get_all_tickets")]
        public async Task<ActionResult<IList<TicketDTO>>> GetAllTickets()
        {
            AuthenticateAndGetTokenAndGetFacade(out LoginToken<AirlineCompany> tokenAirline,
                                                                                   out LoggedsInAirlineFacade fasadeAirline);
            IList<Ticket> tickets = null;

            try
            {
                tickets = await Task.Run(() => fasadeAirline.GetAllTickets(tokenAirline));
            }
            catch (IllegalFlightParameter ex)
            {
                return StatusCode(400, $"{{ error: \"{ex.Message}\" }}");
            }
            if (tickets == null)
            {
                return StatusCode(204, "{ }");
            }
            TicketProfile ticketProfile = new TicketProfile(out MapperConfiguration config);
            var m_mapper = new Mapper(config);
            List<TicketDTO> ticketDTOs = new List<TicketDTO>();

            foreach (Ticket ticket in tickets)
            {
                TicketDTO ticketDTO = m_mapper.Map<TicketDTO>(ticket);
                ticketDTOs.Add(ticketDTO);
            }
            return Ok(JsonConvert.SerializeObject(ticketDTOs, Formatting.Indented));
        }
        //************************************************************
        //******************** GET: api/get_all_flights ********************
        //************************************************************
        /// <summary>
        /// Get all tickets
        /// </summary>
        /// <response code="200">The request was fulfilled</response>
        /// <response code="204">Server has received the request but there is no information to send back</response>
        /// <response code="400">The request had bad syntax or was inherently impossible to be satisfied</response> 
        /// <response code="401">The request has not been applied because it lacks valid authentication credentials for the target resource</response> 
        /// <response code="404">The server has not found anything matching the URI given</response> 
        /// <response code="500">The server encountered an unexpected condition which prevented it from fulfilling the request</response> 
        /// <response code="501">The server does not support the facility required</response> 
        ///
        [HttpGet("get_all_flights")]
        public async Task<ActionResult<IList<FlightDTO>>> GetAllFlights()
        {
            AuthenticateAndGetTokenAndGetFacade(out LoginToken<AirlineCompany> tokenAirline,
                                                                                   out LoggedsInAirlineFacade fasadeAirline);
            IList<Flight> flights = null;

            try
            {
                flights = await Task.Run(() => fasadeAirline.GetAllFlights());
            }
            catch (IllegalFlightParameter ex)
            {
                return StatusCode(400, $"{{ error: \"{ex.Message}\" }}");
            }
            if (flights == null)
            {
                return StatusCode(204, "{ }");
            }
            FlightProfile flightProfile = new FlightProfile(out MapperConfiguration config);
            var m_mapper = new Mapper(config);
            List<FlightDTO> flightDTOs = new List<FlightDTO>();

            foreach (Flight flight in flights)
            {
                FlightDTO flightDTO = m_mapper.Map<FlightDTO>(flight);
                flightDTOs.Add(flightDTO);
            }
            return Ok(JsonConvert.SerializeObject(flightDTOs, Formatting.Indented));
        }

        //******************** POST: api/create_flight ********************
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

        //******************** PUT: api/create_flight ********************
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
                                                                          // return BadRequest($"{{ error: {ex.Message} }}"); // 400 + body = ex.Message
            }
            return null;
        }

        //******************** PUT: api/update_flight/{airline_id} ********************
        [HttpPut("update_flight/{airline_id}")]
        public async Task<ActionResult> UpdateFlight([FromBody] AirlineCompany airline)
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
                                                                          // return BadRequest($"{{ error: {ex.Message} }}"); // 400 + body = ex.Message
            }
            return null;
        }


        //******************** PUT: api/change_my_password ********************
        [HttpPut("change_my_password")]
        public async Task<ActionResult> ChangeMyPassword([FromBody] AirlineCompany airline)
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
                                                                          // return BadRequest($"{{ error: {ex.Message} }}"); // 400 + body = ex.Message
            }
            return null;
        }

        //******************** DELETE: api/cancel_flight/{id} ********************
        [HttpDelete("cancel_flight/{id}")]
        public void CancelFlight(int id)
        {
            AuthenticateAndGetTokenAndGetFacade(out LoginToken<AirlineCompany> tokenAirline,
                                                                      out LoggedsInAirlineFacade facadeAirline);
            Flight flight =  facadeAirline.GetFlightById(id);
            facadeAirline.CancelFlight(tokenAirline, flight);
        }





    }
}
