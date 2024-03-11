namespace PMF.API.Entities
{
    public class Part
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Quantity { get; set; }
        public string Material { get; set; }
        public SurfaceType Surface { get; set; }
        public IFormFile Drawing { get; set; }
        public QuickResponseCode QrCode { get; set; }
        public string ActualStorage { get; set; }
        public string DestinationStorage { get; set; }
    }

    public enum SurfaceType
    {
        Painted,
        Galvanised
    }
}
