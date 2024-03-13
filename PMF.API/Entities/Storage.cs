namespace PMF.API.Entities
{
    public class Storage
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Part> Parts { get; set; } = new();
    }
}
