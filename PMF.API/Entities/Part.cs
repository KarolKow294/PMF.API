namespace PMF.API.Entities
{
    public class Part
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Quantity { get; set; }
        public string Material { get; set; }
        public int SurfaceId { get; set; }
        public IFormFile Drawing { get; set; }

        public QuickResponseCode QrCode { get; set; }
        public Storage ActualStorage { get; set; }
        public int ActualStorageId { get; set; }
        public Storage DestinationStorage { get; set; }
        public int DestinationStorageId { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
