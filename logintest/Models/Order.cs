using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace logintest.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
        public float OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
    }
}
