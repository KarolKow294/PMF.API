using System.Drawing;

namespace PMF.API.Entities
{
    public class QuickResponseCode
    {
        public int Id { get; set; }
        public Bitmap Image { get; set; }

        public Part Part { get; set; }
        public int PartId { get; set; }
    }
}
