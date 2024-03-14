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
        public byte[] Drawing { get; set; }
        public string QrCodeData { get; set; }

        public List<Storage> Storages { get; set; } = new ();
        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
