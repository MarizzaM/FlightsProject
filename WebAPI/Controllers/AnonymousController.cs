using AutoMapper;
using FlightsProject;
using FlightsProject.Facade;
using FlightsProject.Login;
using FlightsProject.Mappers;
using FlightsProject.POCO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebAPI.DTO;
using WebAPI.Mappers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnonymousController : ControllerBase
    {
        private readonly IMapper m_mapper;
        private void AuthenticateAndGetFacade(out AnonymousUserFacade facade)
        {
            // after we learned authentication
            // 1. validate token
            // 2. retrieve LoginToken<Customer>
            // 3. get Customer facade

            // before we learn authentication
            // 1. perform login  -- use real user-name + pwd
            // 2. get the token + facade

            facade = FlightsCenterSystem.GetInstance().GetFacade<Anonymous>(null) as AnonymousUserFacade;

        }
        //The status code below will be displayed on swagger, this example taken from the flights project

        /// <summary>
        /// Get all flights
        /// </summary>
        /// <response code="200">The request was fulfilled</response>
        /// <response code="204">Server has received the request but there is no information to send back</response>
        /// <response code="400">The request had bad syntax or was inherently impossible to be satisfied</response> 
        /// <response code="404">The server has not found anything matching the URI given</response> 
        /// <response code="500">The server encountered an unexpected condition which prevented it from fulfilling the request</response> 
        /// <response code="501">The server does not support the facility required</response> 

        // yes
        [HttpGet("get_all_flights")]
        public async Task<ActionResult<IList<FlightDTO>>> GetAllFlights()
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);

            IList <Flight> flights = null;

            try
            {
                flights = await Task.Run(() => facade.GetAllFlights());
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

            foreach (Flight flight in flights) {
                FlightDTO flightDTO = m_mapper.Map<FlightDTO>(flight);
                flightDTOs.Add(flightDTO);
            }
            return Ok(JsonConvert.SerializeObject(flightDTOs, Formatting.Indented));
        }

        /// <summary>
        /// Get all airline companies
        /// </summary>
        /// <response code="200">The request was fulfilled</response>
        /// <response code="204">Server has received the request but there is no information to send back</response>
        /// <response code="400">The request had bad syntax or was inherently impossible to be satisfied</response> 
        /// <response code="404">The server has not found anything matching the URI given</response> 
        /// <response code="500">The server encountered an unexpected condition which prevented it from fulfilling the request</response> 
        /// <response code="501">The server does not support the facility required</response> 

        // yes
        [HttpGet("get_all_airline_companies")]
        public async Task<ActionResult<IList<AirlineCompanyDTO>>> GetAllAirlineCompanies()
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);

            IList<AirlineCompany> airlines = null;

            try
            {
                airlines = await Task.Run(() => facade.GetAllAirlineCompanies());
            }
            catch (IllegalFlightParameter ex)
            {
                return StatusCode(400, $"{{ error: \"{ex.Message}\" }}");
            }
            if (airlines == null)
            {
                return StatusCode(204, "{ }");
            }
            AirlineProfile airlineProfile = new AirlineProfile(out MapperConfiguration config);
            var m_mapper = new Mapper(config);
            List<AirlineCompanyDTO> airlineCompanyDTOs = new List<AirlineCompanyDTO>();

            foreach (AirlineCompany airline in airlines)
            {
                AirlineCompanyDTO airlineCompanyDTO = m_mapper.Map<AirlineCompanyDTO>(airline);
                airlineCompanyDTOs.Add(airlineCompanyDTO);
            }
            return Ok(JsonConvert.SerializeObject(airlineCompanyDTOs, Formatting.Indented));
        }

        /// <summary>
        ///Get all flights vacancy
        /// </summary>
        /// <response code="200">The request was fulfilled</response>
        /// <response code="204">Server has received the request but there is no information to send back</response>
        /// <response code="400">The request had bad syntax or was inherently impossible to be satisfied</response> 
        /// <response code="404">The server has not found anything matching the URI given</response> 
        /// <response code="500">The server encountered an unexpected condition which prevented it from fulfilling the request</response> 
        /// <response code="501">The server does not support the facility required</response> 

        //yes
        [HttpGet("get_all_flights_vacancy")]
        public async Task<ActionResult<Dictionary<FlightDTO, int>>> GetAllFlightsVacancy()
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);

            Dictionary<Flight, int> flights = null;

            try
            {
                flights = await Task.Run(() => facade.GetAllFlightsVacancy());
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

            foreach (var flight in flights)
            {
                FlightDTO flightDTO = m_mapper.Map<FlightDTO>(flight.Key);
                flightDTOs.Add(flightDTO);
            }
            return Ok(JsonConvert.SerializeObject(flightDTOs, Formatting.Indented));
        }

        /// <summary>
        ///Get flight by id
        /// </summary>
        /// <response code="200">The request was fulfilled</response>
        /// <response code="204">Server has received the request but there is no information to send back</response>
        /// <response code="400">The request had bad syntax or was inherently impossible to be satisfied</response> 
        /// <response code="404">The flight id doesn't point to existing flight</response> 
        /// <response code="500">The server encountered an unexpected condition which prevented it from fulfilling the request</response> 
        /// <response code="501">The server does not support the facility required</response> 

        // yes
        [HttpGet("get_flight/{id}")]
        public async Task<ActionResult<FlightDTO>> GetFlightById(int id)
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);

            Flight flight = null;
            try
            {
                flight = await Task.Run(() => facade.GetFlightById(id));
            }
            catch (IllegalFlightParameter ex)
            {
                return StatusCode(400, $"{{ error: \"{ex.Message}\" }}"); 
            }
            if (flight == null)
            {
                return StatusCode(204, "{ }");
            }
            FlightProfile flightProfile = new FlightProfile(out MapperConfiguration config);
            var mapper = new Mapper(config);
            FlightDTO flightDTO = mapper.Map<FlightDTO>(flight);

            return Ok(JsonConvert.SerializeObject(flightDTO, Formatting.Indented));
        }

        /// <summary>
        ///Get flight by depatrure date
        /// </summary>
        /// <response code="200">The request was fulfilled</response>
        /// <response code="204">Server has received the request but there is no information to send back</response>
        /// <response code="400">The request had bad syntax or was inherently impossible to be satisfied</response> 
        /// <response code="404">The depatrure date of the flight doesn't point to existing flight</response> 
        /// <response code="500">The server encountered an unexpected condition which prevented it from fulfilling the request</response> 
        /// <response code="501">The server does not support the facility required</response> 

        //no
        [HttpGet("get_flights_by_depatrure_date/{departureDate}")]
        public async Task<ActionResult<IList<FlightDTO>>> GetFlightsByDepatrureDate(DateTime departureDate)
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);

            IList<Flight> flights = null;
            try
            {
                flights = await Task.Run(() => facade.GetFlightsByDepatrureDate(departureDate));
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

        /// <summary>
        ///Get flight by landing date
        /// </summary>
        /// <response code="200">The request was fulfilled</response>
        /// <response code="204">Server has received the request but there is no information to send back</response>
        /// <response code="400">The request had bad syntax or was inherently impossible to be satisfied</response> 
        /// <response code="404">The landing date of the flight doesn't point to existing flight</response> 
        /// <response code="500">The server encountered an unexpected condition which prevented it from fulfilling the request</response> 
        /// <response code="501">The server does not support the facility required</response> 

        //no
        [HttpGet("get_flights_by_landing_date/{landingDate}")]
        public async Task<ActionResult<IList<FlightDTO>>> GetFlightsByLandingDate(DateTime landingDate)
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);

            IList<Flight> flights = null;
            try
            {
                flights = await Task.Run(() => facade.GetFlightsByLandingDate(landingDate));
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

        /// <summary>
        ///Get flight by destination country
        /// </summary>
        /// <response code="200">The request was fulfilled</response>
        /// <response code="204">Server has received the request but there is no information to send back</response>
        /// <response code="400">The request had bad syntax or was inherently impossible to be satisfied</response> 
        /// <response code="404">The destination country of the flight doesn't point to existing flight</response> 
        /// <response code="500">The server encountered an unexpected condition which prevented it from fulfilling the request</response> 
        /// <response code="501">The server does not support the facility required</response> 

        //yes
        [HttpGet("get_flights_by_destination_country/{destination_Country_Id}")]
        public async Task<ActionResult<IList<FlightDTO>>> GetFlightsByDestinationCountry(int destination_Country_Id)
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);

            IList<Flight> flights = null;
            try
            {
                flights = await Task.Run(() => facade.GetFlightsByDestinationCountry(destination_Country_Id));
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

        /// <summary>
        ///Get flight by origin country
        /// </summary>
        /// <response code="200">The request was fulfilled</response>
        /// <response code="204">Server has received the request but there is no information to send back</response>
        /// <response code="400">The request had bad syntax or was inherently impossible to be satisfied</response> 
        /// <response code="404">The origin country of the flight doesn't point to existing flight</response> 
        /// <response code="500">The server encountered an unexpected condition which prevented it from fulfilling the request</response> 
        /// <response code="501">The server does not support the facility required</response> 

        //yes
        [HttpGet("get_flights_by_origin_country/{origin_Country_Id}")]
        public async Task<ActionResult<IList<FlightDTO>>> GetFlightsByOriginCountry(int origin_Country_Id)
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);

            IList<Flight> flights = null;
            try
            {
                flights = await Task.Run(() => facade.GetFlightsByOriginCountry(origin_Country_Id));
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
    }
}
