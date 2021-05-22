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
                return StatusCode(400, $"{{ error: \"{ex.Message}\" }}"); // 400 + body = ex.Message
                //return BadRequest($"{{ error: {ex.Message} }}"); // 400 + body = ex.Message
            }
            //return StatusCode(200, result);
            if (result == null)
            {
                return StatusCode(204, "{ }");
            }
            return Ok(result);
        }

        // POST api/<AnonymousFacadeApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AnonymousFacadeApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AnonymousFacadeApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
