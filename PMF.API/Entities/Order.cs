
namespace PMF.API.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public List<Part> Parts { get; set; } = new();
    }
}
