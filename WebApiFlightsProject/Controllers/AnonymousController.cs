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
        /// Get all weather forecasts
        /// </summary>
        /// <response code="204">The airline company has been updated successfully</response>
        /// <response code="400">If the airline company id is different between the url and the body</response> 
        /// <response code="401">If the user is not authenticated as administrator</response> 
        /// <response code="404">If the country id doesn't point to existing country</response> 
        /// <response code="409">If there is another airline company with same name</response> 


        // yes
        [HttpGet("get_all_flights")]
        public IList<Flight> GetAllFlights()
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);
            IList<Flight> result = facade.GetAllFlights();
            return result;
        }

        [HttpGet("get_all_airline_companies")]
        public IList<AirlineCompany> GetAllAirlineCompanies()
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);
            IList<AirlineCompany> result = facade.GetAllAirlineCompanies();
            return result;
        }

        [HttpGet("get_all_flights_vacancy")]
        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);
            Dictionary<Flight, int> result = facade.GetAllFlightsVacancy();
            return result;
        }

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
            return null;
        }

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
            return null;
        }

        [HttpGet("get_flights_by_destination_country/{countryCode}")]
        public async Task<ActionResult<Flight>> GetFlightsByDestinationCountry(int countryCode)
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);

            IList<Flight> result = null;
            try
            {
                result = await Task.Run(() => facade.GetFlightsByDestinationCountry(countryCode));
            }
            catch (IllegalFlightParameter ex)
            {
                return StatusCode(400, $"{{ error: \"{ex.Message}\" }}");
            }
            if (result == null)
            {
                return StatusCode(204, "{ }");
            }
            return null;
        }

        [HttpGet("get_flights_by_origin_country/{countryCode}")]
        public async Task<ActionResult<Flight>> GetFlightsByOriginCountry(int countryCode)
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);

            IList<Flight> result = null;
            try
            {
                result = await Task.Run(() => facade.GetFlightsByOriginCountry(countryCode));
            }
            catch (IllegalFlightParameter ex)
            {
                return StatusCode(400, $"{{ error: \"{ex.Message}\" }}");
            }
            if (result == null)
            {
                return StatusCode(204, "{ }");
            }
            return null;
        }
    }
}
