using CsvHelper.Configuration.Attributes;

namespace PMF.API.Models
{
    public class CsvPartDto
    {
        [Name("Nazwa zlecenia")]
        public string OrderName { get; set; }
        [Name("Numer zlecenia")]
        public string OrderNumber { get; set; }
        [Name("Nazwa części")]
        public string Name { get; set; }
        [Name("Kod")]
        public string Code { get; set; }
        [Name("Ilość")]
        public int Quantity { get; set; }
        [Name("Materiał")]
        public string Material { get; set; }
        [Name("Powierzchnia")]
        public string Surface { get; set; }
        [Name("Aktualny magazyn")]
        public string ActualStorage { get; set; }
        [Name("Docelowy magazyn")]
        public string DestinationStorage { get; set; }
    }
}
