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

            // yes
            [HttpGet("getallflights")]
        public IList<Flight> GetAllFlights()
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);
            IList<Flight> result = facade.GetAllFlights();
            return result;
        }

        [HttpGet("getallairlinecompanies")]
        public IList<AirlineCompany> GetAllAirlineCompanies()
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);
            IList<AirlineCompany> result = facade.GetAllAirlineCompanies();
            return result;
        }

        [HttpGet("getallflightsvacancy")]
        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            AuthenticateAndGetFacade(out AnonymousUserFacade facade);
            Dictionary<Flight, int> result = facade.GetAllFlightsVacancy();
            return result;
        }

        // yes
        [HttpGet("getflight/{id}")]
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

        [HttpGet("getflightsbydepatruredate/{departureDate}")]
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

        [HttpGet("getflightsbylandingdate/{landingDate}")]
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

        [HttpGet("cetflightsbydestinationcountry/{countryCode}")]
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

        [HttpGet("getflightsbyorigincountry/{countryCode}")]
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
