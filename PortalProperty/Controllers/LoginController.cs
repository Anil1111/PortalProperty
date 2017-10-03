using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PortalProperty.Models;

namespace PortalProperty.Controllers
{
    public class LoginController : ApiController
    {
        [HttpGet]
        public Users getUser(string email, string password)
        {
            DataAccess db = new DataAccess();
            return db.getUser(email, password);
        }
    }
}
