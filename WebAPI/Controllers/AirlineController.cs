﻿using AutoMapper;
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
        //************************************************************
        //******************** POST: api/create_flight ********************
        //************************************************************
        /// <summary>
        /// Create flight
        /// </summary>
        /// <response code="201">Success</response>
        /// <response code="204">Server has received the request but there is no information to send back</response>
        /// <response code="400">The request had bad syntax or was inherently impossible to be satisfied</response> 
        /// <response code="401">The request has not been applied because it lacks valid authentication credentials for the target resource</response> 
        /// <response code="404">The server has not found anything matching the URI given</response> 
        /// <response code="500">The server encountered an unexpected condition which prevented it from fulfilling the request</response> 
        /// <response code="501">The server does not support the facility required</response> 
        ///
        [HttpPost("create_flight")]
        public async Task<ActionResult> CreateFlight([FromBody] FlightCreationDTO flightCreationDTO)
        {

            AuthenticateAndGetTokenAndGetFacade(out LoginToken<AirlineCompany> tokenAirline,
                                                                      out LoggedsInAirlineFacade facadeAirline);
            FlightCreationProfile flightCreation = new FlightCreationProfile(out MapperConfiguration configCreation);
            var mapper = new Mapper(configCreation);
            Flight flight = mapper.Map<Flight>(flightCreationDTO);

            try
            {
                await Task.Run(() => facadeAirline.CreateFlight(tokenAirline, flight));
            }
            catch (IllegalFlightParameter ex)
            {
                return StatusCode(400, $"{{ error: \"{ex.Message}\" }}"); // 400 + body = ex.Message
                //return BadRequest($"{{ error: {ex.Message} }}"); // 400 + body = ex.Message
            }


            FlightProfile flightProfile = new FlightProfile(out MapperConfiguration config);
            var m_mapper = new Mapper(config);
            FlightDTO flightDTO = m_mapper.Map<FlightDTO>(flight);

            return new CreatedResult("/api/anonymous/get_flight/" + flightDTO.Id, flightCreationDTO);
        }
        //****************************************************************************
        //******************** PUT: api/mofidy_airline_details/{airline_id} ********************
        //****************************************************************************
        /// <summary>
        /// Mofidy Airline Ddetails
        /// </summary>
        /// <response code="201">Success</response>
        /// <response code="204">Server has received the request but there is no information to send back</response>
        /// <response code="400">The request had bad syntax or was inherently impossible to be satisfied</response> 
        /// <response code="401">The request has not been applied because it lacks valid authentication credentials for the target resource</response> 
        /// <response code="404">The server has not found anything matching the URI given</response> 
        /// <response code="500">The server encountered an unexpected condition which prevented it from fulfilling the request</response> 
        /// <response code="501">The server does not support the facility required</response> 

        [HttpPut("mofidy_airline_details/{airline_id}")]
        //public async Task<ActionResult> MofidyAirlineDetails([FromBody] AirlineCompany airline)
        public async Task<ActionResult> MofidyAirlineDetails([FromBody] AirlineModifyDTO airlineModifyDTO)
        {
            AuthenticateAndGetTokenAndGetFacade(out LoginToken<AirlineCompany> tokenAirline,
                                                                      out LoggedsInAirlineFacade facadeAirline);

            AirlineModifyProfile airlineModify = new AirlineModifyProfile(out MapperConfiguration configModify);
            var mapper = new Mapper(configModify);
            AirlineCompany airline = mapper.Map<AirlineCompany>(airlineModifyDTO);

            try
            {
                await Task.Run(() => facadeAirline.MofidyAirlineDetails(tokenAirline, airline));
            }
            catch (IllegalFlightParameter ex)
            {
                return StatusCode(400, $"{{ error: \"{ex.Message}\" }}");                                                         
            }

            AirlineProfile airlineProfile = new AirlineProfile(out MapperConfiguration config);
            var m_mapper = new Mapper(config);
            AirlineCompanyDTO airlineCompanyDTO = m_mapper.Map<AirlineCompanyDTO>(airline);
            return new CreatedResult("/api/anonymous/get_airline/" + airlineCompanyDTO.Id, airlineCompanyDTO);
        }
        //*********************************************************************
        //******************** PUT: api/update_flight/{flight_id} ********************
        //*********************************************************************
        /// <summary>
        /// Update Flight
        /// </summary>
        /// <response code="201">Success</response>
        /// <response code="204">Server has received the request but there is no information to send back</response>
        /// <response code="400">The request had bad syntax or was inherently impossible to be satisfied</response> 
        /// <response code="401">The request has not been applied because it lacks valid authentication credentials for the target resource</response> 
        /// <response code="404">The server has not found anything matching the URI given</response> 
        /// <response code="500">The server encountered an unexpected condition which prevented it from fulfilling the request</response> 
        /// <response code="501">The server does not support the facility required</response> 
        [HttpPut("update_flight/{flight_id}")]
        public async Task<ActionResult> UpdateFlight([FromBody] FlightDTO flightDTO)
        {
            AuthenticateAndGetTokenAndGetFacade(out LoginToken<AirlineCompany> tokenAirline,
                                                                      out LoggedsInAirlineFacade facadeAirline);
            FlightMogifyProfile flightMogify = new FlightMogifyProfile(out MapperConfiguration config);
            var mapper = new Mapper(config);
            Flight flight = mapper.Map<Flight>(flightDTO);
            try
            {
                await Task.Run(() => facadeAirline.UpdateFlight(tokenAirline, flight));
            }
            catch (IllegalFlightParameter ex)
            {
                return StatusCode(400, $"{{ error: \"{ex.Message}\" }}");
            }
            FlightProfile flightProfile = new FlightProfile(out config);
            var m_mapper = new Mapper(config);
             flightDTO = m_mapper.Map<FlightDTO>(flight);

            return new CreatedResult("/api/anonymous/get_flight/" + flight.Id, flightDTO); 
        }


        //******************************************************************
        //******************** DELETE: api/cancel_flight/{id} ********************
        //******************************************************************
        /// <summary>
        /// Create flight
        /// </summary>
        /// <response code="201">Success</response>
        /// <response code="204">Server has received the request but there is no information to send back</response>
        /// <response code="400">The request had bad syntax or was inherently impossible to be satisfied</response> 
        /// <response code="401">The request has not been applied because it lacks valid authentication credentials for the target resource</response> 
        /// <response code="404">The server has not found anything matching the URI given</response> 
        /// <response code="500">The server encountered an unexpected condition which prevented it from fulfilling the request</response> 
        /// <response code="501">The server does not support the facility required</response> 
        ///
        [HttpDelete("cancel_flight/{id}")]
        public async Task<ActionResult> CancelFlight(int id)
        {
            AuthenticateAndGetTokenAndGetFacade(out LoginToken<AirlineCompany> tokenAirline,
                                                                      out LoggedsInAirlineFacade facadeAirline);

            Flight flight = facadeAirline.GetFlightById(id);

            try
            {
                await Task.Run(() => facadeAirline.CancelFlight(tokenAirline, flight));
            }
            catch (IllegalFlightParameter ex)
            {
                return StatusCode(400, $"{{ error: \"{ex.Message}\" }}"); 
            }
            return Ok();
        }
    }
}
