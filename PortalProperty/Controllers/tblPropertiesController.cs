using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using PortalProperty.Models;

namespace PortalProperty.Controllers
{
    public class tblPropertiesController : ApiController
    {
        private Database1Entities db = new Database1Entities();

        // GET: api/tblProperties
        //public IQueryable<tblProperty> GettblProperties()
        //{
        //    return db.tblProperties;
        //}

        // GET: api/tblProperties/5
        [ResponseType(typeof(tblProperty))]
        public IHttpActionResult GettblProperty(int id)
        {
            tblProperty tblProperty = db.tblProperties.Find(id);
            if (tblProperty == null)
            {
                return NotFound();
            }

            return Ok(tblProperty);
        }

        // PUT: api/tblProperties/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblProperty(int id, tblProperty tblProperty)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (db)
            {
                var cust = db.tblProperties.Where(g => g.Prop_id.Equals(id)).FirstOrDefault();
                if (cust != null)
                {
                    cust.Prop_Desc = tblProperty.Prop_Desc;
                    cust.Prop_Type = tblProperty.Prop_Type;
                    cust.Prop_Stat = tblProperty.Prop_Stat;
                    cust.BedRoom = tblProperty.BedRoom;
                    cust.BathRoom = tblProperty.BathRoom;
                    cust.Garage = tblProperty.Garage;
                    cust.Price= tblProperty.Price;
                    cust.Cities = tblProperty.Cities;
                    cust.Address = tblProperty.Address;
  
                    var res = db.SaveChanges();

                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }

        // POST: api/tblProperties
        [ResponseType(typeof(tblProperty))]
        public IHttpActionResult PosttblProperty(tblProperty tblProperty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblProperties.Add(tblProperty);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tblPropertyExists(tblProperty.Prop_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tblProperty.Prop_id }, tblProperty);
        }

        // DELETE: api/tblProperties/5
        [ResponseType(typeof(tblProperty))]
        public IHttpActionResult DeletetblProperty(int id)
        {
            tblProperty tblProperty = db.tblProperties.Find(id);
            if (tblProperty == null)
            {
                return NotFound();
            }

            db.tblProperties.Remove(tblProperty);
            db.SaveChanges();

            return Ok(tblProperty);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblPropertyExists(int id)
        {
            return db.tblProperties.Count(e => e.Prop_id == id) > 0;
        }
        
        [Route("api/GetProp")]
       public IEnumerable<PropertyDetail> getInfo()
        {
            GetPropertyInformation_Result oj = new GetPropertyInformation_Result();
            var result =  db.Database.SqlQuery<PropertyDetail>("SELECT Prop_Desc,Prop_Type,Prop_Stat,Price,Cities,Images FROM dbo.tblProperty a, dbo.tblGallery c WHERE a.Prop_id = c.ProperyID");
           
            return result;
        }


    }
}