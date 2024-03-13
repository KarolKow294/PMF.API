using PMF.API.Entities;
using PMF.API.Models;

namespace PMF.API.Services
{
    public class OrderService(PmfDbContext dbContext) : IOrderService
    {
        public List<OrderDto> GetAll()
        {
            var orders = new List<OrderDto>();
            var parts1 = new List<PartDto>();
            var parts2 = new List<PartDto>();

            var part1 = new PartDto()
            {
                Id = 1,
                Name = "płoza",
                Code = "6000-800",
                Quantity = 5,
                Material = "S235",
                Surface = SurfaceType.Painted.ToString(),
                ActualStorage = "lasery",
                DestinationStorage = "FarbaLux"
            };

            var part2 = new PartDto()
            {
                Id = 2,
                Name = "stopa",
                Code = "210-800",
                Quantity = 102,
                Material = "S2355",
                Surface = SurfaceType.Galvanised.ToString(),
                ActualStorage = "krawędziarki",
                DestinationStorage = "FastGalva"
            };

            parts1.Add(part1);
            parts1.Add(part2);

            var part3 = new PartDto()
            {
                Id = 3,
                Name = "blacha",
                Code = "7000-800",
                Quantity = 7,
                Material = "S235",
                Surface = SurfaceType.Painted.ToString(),
                ActualStorage = "lasery",
                DestinationStorage = "FarbaLux"
            };

            var part4 = new PartDto()
            {
                Id = 4,
                Name = "element",
                Code = "210-700",
                Quantity = 99,
                Material = "S2355",
                Surface = SurfaceType.Galvanised.ToString(),
                ActualStorage = "prasy",
                DestinationStorage = "FastGalva"
            };

            parts2.Add(part3);
            parts2.Add(part4);

            var order1 = new OrderDto()
            {
                Id = 1,
                Name = "Przenośnik strefowy",
                Number = "RO/12/04/2024",
                Parts = parts1
            };

            var order2 = new OrderDto()
            {
                Id = 2,
                Name = "Przenośnik pasowy",
                Number = "RO/22/05/2024",
                Parts = parts2
            };

            orders.Add(order1);
            orders.Add(order2);

            return orders;
        }
    }
}
