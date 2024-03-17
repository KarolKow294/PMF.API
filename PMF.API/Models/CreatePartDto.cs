namespace PMF.API.Models
{
    public class CreatePartDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Quantity { get; set; }
        public string Material { get; set; }
        public int SurfaceId { get; set; }
        public byte[] File { get; set; }
        public int ActualStorageId { get; set; }
        public int DestinationStorageId { get; set; }
        public int OrderId { get; set; }
    }
}
