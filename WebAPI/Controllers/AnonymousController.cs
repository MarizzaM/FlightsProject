using FlightsProject;
using FlightsProject.Facade;
using FlightsProject.Login;
using FlightsProject.POCO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnonymousController : ControllerBase
    {
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
        public IList<Flight> GetAllFlights()
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);
            IList<Flight> result = facade.GetAllFlights();
            return result;
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
        public IList<AirlineCompany> GetAllAirlineCompanies()
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);
            IList<AirlineCompany> result = facade.GetAllAirlineCompanies();
            return result;
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
        public async Task<ActionResult<Dictionary<Flight, int>>> GetAllFlightsVacancy()
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);

            Dictionary<Flight, int> result = null;

            try
            {
                result = await Task.Run(() => facade.GetAllFlightsVacancy());
            }
            catch (IllegalFlightParameter ex)
            {
                return StatusCode(400, $"{{ error: \"{ex.Message}\" }}");
            }
            if (result == null)
            {
                return StatusCode(204, "{ }");
            }

            return Ok(JsonSerializer.Serialize(Newtonsoft.Json.JsonConvert.SerializeObject(result)));
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
        public async Task<ActionResult<Flight>> GetFlightById(int id)
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);

            Flight result = null;
            try
            {
                result = await Task.Run(() => facade.GetFlightById(id));
            }
            catch (IllegalFlightParameter ex)
            {
                return StatusCode(400, $"{{ error: \"{ex.Message}\" }}"); 
            }
            if (result == null)
            {
                return StatusCode(204, "{ }");
            }
            return Ok(result);
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
        public async Task<ActionResult<Flight>> GetFlightsByDepatrureDate(DateTime departureDate)
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);

            IList<Flight> result = null;
            try
            {
                result = await Task.Run(() => facade.GetFlightsByDepatrureDate(departureDate));
            }
            catch (IllegalFlightParameter ex)
            {
                return StatusCode(400, $"{{ error: \"{ex.Message}\" }}");
            }
            if (result == null)
            {
                return StatusCode(204, "{ }");
            }
            return Ok(result);
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
        public async Task<ActionResult<Flight>> GetFlightsByLandingDate(DateTime landingDate)
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);

            IList<Flight> result = null;
            try
            {
                result = await Task.Run(() => facade.GetFlightsByLandingDate(landingDate));
            }
            catch (IllegalFlightParameter ex)
            {
                return StatusCode(400, $"{{ error: \"{ex.Message}\" }}"); 
            }
            if (result == null)
            {
                return StatusCode(204, "{ }");
            }
            return Ok(result);
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
        public async Task<ActionResult<Flight>> GetFlightsByDestinationCountry(int destination_Country_Id)
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);

            IList<Flight> result = null;
            try
            {
                result = await Task.Run(() => facade.GetFlightsByDestinationCountry(destination_Country_Id));
            }
            catch (IllegalFlightParameter ex)
            {
                return StatusCode(400, $"{{ error: \"{ex.Message}\" }}");
            }
            if (result == null)
            {
                return StatusCode(204, "{ }");
            }
            return Ok(result);
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
        public async Task<ActionResult<Flight>> GetFlightsByOriginCountry(int origin_Country_Id)
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);

            IList<Flight> result = null;
            try
            {
                result = await Task.Run(() => facade.GetFlightsByOriginCountry(origin_Country_Id));
            }
            catch (IllegalFlightParameter ex)
            {
                return StatusCode(400, $"{{ error: \"{ex.Message}\" }}");
            }
            if (result == null)
            {
                return StatusCode(204, "{ }");
            }
            return Ok(result);
        }
    }
}
