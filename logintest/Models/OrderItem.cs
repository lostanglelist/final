using logintest.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace logintest.Models
{
    public class OrderItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public float Quantity { get; set; }
        public float Price { get; set; }
        public int OrderId { get; set; }
        public string VGAId { get; set; }
        public string VGAImage { get; set; }
        public string VGAName { get; set; }
        public Order Order { get; set; }
        public ImageModel Image { get; set; }

    }
}
