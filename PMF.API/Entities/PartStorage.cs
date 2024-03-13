namespace PMF.API.Entities
{
    public class PartStorage
    {
        public Part Part { get; set; }
        public int PartId { get; set; }
        public Storage Storage { get; set; }
        public int StorageId { get; set; }
        public string Type { get; set; }
    }
}
