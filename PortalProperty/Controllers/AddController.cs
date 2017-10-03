using PortalProperty.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;


namespace PortalProperty.Controllers
{
    public class AddController : ApiController
    {
        // GET: api/Add
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Add/5
        public HttpResponseMessage Get(Users email, Users password)
        {
            tblUsersController tbl = new tblUsersController();
            long  iden;
            iden = tbl.GettblUser(email,password);
            email.id = iden;
            password.id = iden;
            HttpResponseMessage resp = Request.CreateResponse(HttpStatusCode.Created);
            resp.Headers.Location = new Uri(Request.RequestUri, string.Format("tblUser/{0}", email,password));

            return resp;
        }

        // POST: api/Add
        public HttpResponseMessage POST([FromBody] Users id)
        {
            DataAccess da = new DataAccess();
            long iden;
            iden = da.User(id);
            id.id = iden;
            HttpResponseMessage respond = Request.CreateResponse(HttpStatusCode.Created);
            respond.Headers.Location = new Uri(Request.RequestUri, string.Format("tblUser/{0}", id));

            return respond;
        }

       
        // PUT: api/Add/5
        //public HttpResponseMessage GET([FromBody]Users email, Users password)
        //{
        //    return email;
        //}

        // DELETE: api/Add/5
        public void Delete(int id)
        {
        }
    }
}
