using FlightsProject;
using FlightsProject.Login;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public abstract class FlightControllerBase<T> : ControllerBase where T : IUser, new()
    {
        protected LoginToken<T> GetLoginToken()
        {
            string userName = User.Claims.First(_ => _.Type ==
                                                "username").Value;
            LoginToken<T> login_token = new LoginToken<T>()
            {
                User = new T()
                {
                    Username = userName,
                    Password = "JWT"
                }
            };
            return login_token;
        }
    }
}
