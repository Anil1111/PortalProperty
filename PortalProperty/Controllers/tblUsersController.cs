using System;
using System.Data;
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
    public class tblUsersController : ApiController
    {
        private Database1Entities db = new Database1Entities();

        internal long GettblUser(Users email, Users password)
        {
            throw new NotImplementedException();
        }

        // GET: api/tblUsers
        //public IQueryable<tblUser> GettblUsers()
        //{
        //    return db.tblUsers;
        //}

        public tblUser getUser(int ID)
        {
            var data = db.tblUsers.Where(tblUser => tblUser.Id.Equals(ID)).FirstOrDefault();
            if (data != null)
            {
                tblUser tlUser = new tblUser();
                tlUser.Id = data.Id;
                tlUser.firstname = data.firstname;
                tlUser.lastname = data.lastname;
                tlUser.email = data.email;
                tlUser.password = data.password;

                return tlUser;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

        }
        // GET: api/tblUsers/5
        [ResponseType(typeof(tblUser))]
        public IHttpActionResult GettblUser(string email, string password)
        {
            var cust = db.tblUsers.Where(tblUser => tblUser.email.Equals(email) && tblUser.password.Equals(password)).FirstOrDefault();

            if (cust.email == null && cust.password == null)
            {
                return (null);
            }
            else
            {
                return Ok(cust);
            }
        }

        [System.Web.Http.HttpPut]
        // PUT: api/tblUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblUser(int id, tblUser tblUser)
        {

            Console.WriteLine("Hello");
            tblUser oldDetails = tblUser;


            if (!ModelState.IsValid)
            {
                return BadRequest("Not valid data");
            }


            using (db)
            {
                var cust = db.tblUsers.Where(g => g.Id.Equals(id)).FirstOrDefault();
                if (cust != null)
                {
                    cust.firstname = tblUser.firstname;
                    cust.lastname = tblUser.lastname;
                    cust.email = tblUser.email;
                    cust.password = tblUser.password;
                    cust.contact = tblUser.contact;
                    cust.gender = tblUser.gender;

                    var res = db.SaveChanges();

                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }

        // POST: api/tblUsers
        [ResponseType(typeof(tblUser))]
        public IHttpActionResult PosttblUser(tblUser tblUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            tblUser.Id = new Random().Next();
            db.tblUsers.Add(tblUser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tblUserExists(tblUser.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tblUser.Id }, tblUser);
        }

        // DELETE: api/tblUsers/5
        [ResponseType(typeof(tblUser))]
        public IHttpActionResult DeletetblUser(int id)
        {
            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return NotFound();
            }

            db.tblUsers.Remove(tblUser);
            db.SaveChanges();

            return Ok(tblUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblUserExists(int id)
        {
            return db.tblUsers.Count(e => e.Id == id) > 0;
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Emails")]
        public string SenderMail(tblMail mail)
        {
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;

            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential("mokoneg11@gmail.com", "GoMo13#$");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(mail.sEmail);
            msg.To.Add(new MailAddress("mokoneg11@gmail.com"));

            msg.Subject = "Enquire about a property";
            msg.IsBodyHtml = true;

            msg.Body = string.Format("<html><head><head><body><b> Name: </ b > " + mail.sender + " < br/>< br/> " + " <b> Email: </b > " + mail.sEmail + " <br/>< br/> " + " <b> Mobile Number: </b > " + mail.senderContact + " < br/>< br/> " + mail.messages + " </body></html> ");
            try
            {
                client.Send(msg);
                return "Email has been sent";
            }
            catch (Exception ex)
            {

                return "An error:" + ex.ToString() + "occured";
            }
        }
    }
}