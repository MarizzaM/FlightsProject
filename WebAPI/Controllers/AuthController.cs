using FlightsProject;
using FlightsProject.Facade;
using FlightsProject.Login;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DTO;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        [HttpPost("token")]
        public async Task<ActionResult> GetToken([FromBody] UserDetailsDTO userDetails)
             
        {
            // 1) try login, with userDetails
            //ILoginService loginService = new LoginService();
          //  loginService.TryAdminLogin(userDetails.Name, userDetails.Password, out LoginToken<Admin> tokenAdmin);
            //facadeAdmin = FlightsCenterSystem.GetInstance().GetFacade(tokenAdmin) as LoggedInAdministratorFacade;


            try
            {
                await Task.Run(() => FlightsCenterSystem.GetInstance().Login(userDetails.Name, userDetails.Password));
            }
            catch (IllegalFlightParameter ex)
            {
                return Unauthorized("login failed");
            }

            // 2) create key
            // security key
            string securityKey =
       "this_is_our_supper_long_security_key_for_token_validation_project_2018_09_07$smesk.in";

            // symmetric security key
            var symmetricSecurityKey = new
                SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            // signing credentials
            var signingCredentials = new
                  SigningCredentials(symmetricSecurityKey,
                  SecurityAlgorithms.HmacSha256Signature);

            // 3) create claim for specific role
            // add claims
            var claims = new List<Claim>();
            // create claim according to login -- Airline or Admin or ...
            claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            claims.Add(new Claim(ClaimTypes.Role, "AirlineCompany"));
            claims.Add(new Claim("username", "userDetails.Name"));
         //   claims.Add(new Claim("Id", "110"));

            // 4) create token
            var token = new JwtSecurityToken(
            issuer: "smesk.in", // change to something better
            audience: "readers", // change to something better
            expires: DateTime.Now.AddHours(1), // should be configurable
            signingCredentials: signingCredentials,
            claims: claims);

            // 5) return token
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
