﻿using System.Drawing;

namespace PMF.API.Models
{
    public class PartDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Quantity { get; set; }
        public string Material { get; set; }
        public string Surface { get; set; }
        public byte[] Drawing { get; set; }
        public string QrDataImage { get; set; }
        public string ActualStorage { get; set; }
        public string DestinationStorage { get; set; }
        public int OrderId { get; set; }
    }
}
