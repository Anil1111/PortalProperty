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
using System.Web;
using System.IO;

namespace PortalProperty.Controllers
{
    public class tblGalleriesController : ApiController
    {
        private Database1Entities db = new Database1Entities();
        
        //[ResponseType(typeof(tGetPropertyInformation))]
        //public IEnumerable<db.GetPropertyInformation> get()
        //{

        //}
        // GET: api/tblGalleries
        public IQueryable<tblGallery> GettblGalleries()
        {
            return db.tblGalleries;
        }

        // GET: api/tblGalleries/5
        [ResponseType(typeof(tblGallery))]
        public IHttpActionResult GettblGallery(int id)
        {
            tblGallery tblGallery = db.tblGalleries.Find(id);
            if (tblGallery == null)
            {
                return NotFound();
            }

            return Ok(tblGallery);
        }

        public String POST()
        {
            int counter = 0;

            //collection files
            System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            tblGallery gal = new tblGallery();
            
            String Status = "";
            for (int i = 0; i < files.Count; i++)
            {

                //get the posted file
                System.Web.HttpPostedFile file = files[i];

                string fileName = new FileInfo(file.FileName).Name;

                //String Id = url.Split('=')[1];

                if (file.ContentLength > 0)
                {
                    Guid id = Guid.NewGuid();

                    string modifiedFileName = id.ToString() + "_" + fileName;

                    byte[] imageb = new byte[file.ContentLength];
                    file.InputStream.Read(imageb, 0, file.ContentLength);

                    gal.Images = imageb;
                    //gal.ID = new Random().Next();
                    db.tblGalleries.Add(gal);
                    db.SaveChanges();
                    counter++;

                }

            }

            if (counter > 0)
            {
                return Status;
            }
            return "Upload Failed";
        }
    
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblGalleryExists(int id)
        {
            return db.tblGalleries.Count(e => e.ID == id) > 0;
        }
     //   [Route("api/GetImages")]
     //public IEnumerable<Procedure_Result> images()
     //   {
     //       return db.Procedure().AsEnumerable();
     //   }
      
    }
}