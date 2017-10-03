using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using PortalProperty.Models;
using System.IO;

namespace PortalProperty.Controllers
{
    public class tblGalleryController : ApiController
    {
        private Database1Entities1 db = new Database1Entities1();

        [HttpPost]
        [Route("api/tblGalleries")]
        public IHttpActionResult upload(tblGallery tblg)
        {
            int countUpload = 0;
            string sPath = System.Web.Hosting.HostingEnvironment.MapPath("/Galleries/");
            System.Web.HttpFileCollection file = System.Web.HttpContext.Current.Request.Files;

            for (int g = 0; g < file.Count; g++)
            {
                System.Web.HttpPostedFile files = file[g];

                string Filename = new FileInfo(files.FileName).Name;

              if (files.ContentLength > 0)
                {
                    Guid id = Guid.NewGuid();

                    string modifiedFilename = id.ToString() + " " + Filename;

                    if (!File.Exists(sPath + Path.GetFileName(modifiedFilename)))
                    {
                        files.SaveAs(sPath + Path.GetFileName(modifiedFilename));
                        countUpload++;
                        //tblg.Image_id = new Random().Next();
                        db.tblGalleries.Add(new tblGallery() { ImageUrl = id, FileName = "/Galleries/" + modifiedFilename, Title = Filename });
                    }
                }
            }
            if (countUpload > 0)
            {
                db.SaveChanges();
                return Ok("Uploaded Successfully");
            }
            return InternalServerError();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
