namespace PMF.API.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public List<PartDto> Parts { get; set; }
    }
}
