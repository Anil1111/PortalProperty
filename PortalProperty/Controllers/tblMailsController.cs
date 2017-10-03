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
using System.Net.Mail;

namespace PortalProperty.Controllers
{
    public class tblMailsController : ApiController
    {
        private Database1Entities db = new Database1Entities();

        // GET: api/tblMails
        public IQueryable<tblMail> GettblMails()
        {
            return db.tblMails;
        }

        // GET: api/tblMails/5
        [ResponseType(typeof(tblMail))]
        public IHttpActionResult GettblMail(int id)
        {
            tblMail tblMail = db.tblMails.Find(id);
            if (tblMail == null)
            {
                return NotFound();
            }

            return Ok(tblMail);
        }

        // PUT: api/tblMails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblMail(int id, tblMail tblMail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblMail.Id)
            {
                return BadRequest();
            }

            db.Entry(tblMail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblMailExists(id))
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

        // POST: api/tblMails
        [ResponseType(typeof(tblMail))]
        public IHttpActionResult PosttblMail(tblMail tblMail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblMails.Add(tblMail);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblMail.Id }, tblMail);
        }

        // DELETE: api/tblMails/5
        [ResponseType(typeof(tblMail))]
        public IHttpActionResult DeletetblMail(int id)
        {
            tblMail tblMail = db.tblMails.Find(id);
            if (tblMail == null)
            {
                return NotFound();
            }

            db.tblMails.Remove(tblMail);
            db.SaveChanges();

            return Ok(tblMail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblMailExists(int id)
        {
            return db.tblMails.Count(e => e.Id == id) > 0;
        }
    }
}