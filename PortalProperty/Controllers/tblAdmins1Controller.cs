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
    public class tblAdmins1Controller : ApiController
    {
        private Database1Entities db = new Database1Entities();

        // GET: api/tblAdmins1
        public IQueryable<tblAdmin> GettblAdmins()
        {
            return db.tblAdmins;
        }

        // GET: api/tblAdmins1/5
        [ResponseType(typeof(tblAdmin))]
        public IHttpActionResult GettblAdmin(string username, string PassWord)
        {
            var cust = db.tblAdmins.Where(tblAd => tblAd.UserName.Equals(username) && tblAd.Password.Equals(PassWord)).FirstOrDefault();

            if (cust.UserName == null && cust.Password == null)
            {
                return (null);
            }
            else
            {
                return Ok(cust);
            }
           
        }

        // PUT: api/tblAdmins1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblAdmin(int id, tblAdmin tblAdmin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblAdmin.Admin_ID)
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

        // POST: api/tblAdmins1
        [ResponseType(typeof(tblAdmin))]
        public IHttpActionResult PosttblAdmin(tblAdmin tblAdmin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblAdmins.Add(tblAdmin);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblAdmin.Admin_ID }, tblAdmin);
        }

        // DELETE: api/tblAdmins1/5
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
            return db.tblAdmins.Count(e => e.Admin_ID == id) > 0;
        }
    }
}