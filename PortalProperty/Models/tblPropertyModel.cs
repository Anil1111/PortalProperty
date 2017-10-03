using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalProperty.Models
{
    public class tblPropertyModel
    {
        public int PropId { get; set; }

        public string PDescription { get; set; }
        public string PropType { get; set; }
        public string status { get; set; }
        public int baRoom { get; set; }
        public int beRoom { get; set; }
        public byte[] Picture { get; set; }
        public int LocationID { get; set; }
    }
}