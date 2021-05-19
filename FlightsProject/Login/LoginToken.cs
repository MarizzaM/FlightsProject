﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsProject.Login
{
    public class LoginToken<T>: ILoginToken where T : IUser
    {
            public T User { get; set; }
    }
}
