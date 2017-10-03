namespace PortalProperty.Controllers
{
    public class PropertyDetail
    {
        public string Prop_Desc { set; get; }
        public string Prop_Type { set; get; }
        public string Prop_Stat { set; get; }
        public double Price { set; get; }
        public string Cities { set; get; }
        public byte[] Images { set; get; }
        public PropertyDetail() { }
    }
}