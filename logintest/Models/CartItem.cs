using logintest.Areas.Identity.Data;

namespace logintest.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public ImageModel image { get; set; }
        public float Quantity { get; set; }
        public string VGAId { get; set; }
    }
}
