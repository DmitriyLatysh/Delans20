using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Delans.Models
{
    public class LoginResponse : Response
    {
        public string SessionId;
        public string ICN;
    }
}