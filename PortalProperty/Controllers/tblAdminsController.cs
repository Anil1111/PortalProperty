using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PortalProperty.Models;

namespace PortalProperty.Controllers
{
    public class tblAdminsController : ApiController
    {
        private Database1Entities1 db = new Database1Entities1();

        //GET: api/tblAdmins
        //public IQueryable<tblAdmin> GettblAdmins()
        //{
        //    return db.tblAdmins;
        //}

        // GET: api/tblAdmins/5
        [ResponseType(typeof(tblAdmin))]
        public IHttpActionResult GettblAdmin(string username, string pass)
        {
                tblAdmin tblAdmins = db.tblAdmins.Find(username, pass);
            var ad= db.tblAdmins.Where(tblAdmin => tblAdmin.UserName.Equals(username) && tblAdmin.Password.Equals(pass)).FirstOrDefault();

            if (ad.UserName == "localhost11@gmail.com" && ad.Password == "tester11")
            {
                return (null);
            }
            else
            {
                string message = "Welcome " + ad.UserName;
                return Ok(ad);
            }
        }

        // PUT: api/tblAdmins/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblAdmin(int id, tblAdmin tblAdmin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblAdmin.AdminID)
            {
                return BadRequest();
            }

            db.Entry(tblAdmin).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblAdminExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

       

        // POST: api/tblAdmins
        [ResponseType(typeof(tblAdmin))]
        public IHttpActionResult PosttblAdmin(tblAdmin tblAdmin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblAdmins.Add(tblAdmin);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tblAdminExists(tblAdmin.AdminID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tblAdmin.AdminID }, tblAdmin);
        }

        // DELETE: api/tblAdmins/5
        [ResponseType(typeof(tblAdmin))]
        public IHttpActionResult DeletetblAdmin(int id)
        {
            tblAdmin tblAdmin = db.tblAdmins.Find(id);
            if (tblAdmin == null)
            {
                return NotFound();
            }

            db.tblAdmins.Remove(tblAdmin);
            db.SaveChanges();

            return Ok(tblAdmin);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblAdminExists(int id)
        {
            return db.tblAdmins.Count(e => e.AdminID == id) > 0;
        }
    }
}