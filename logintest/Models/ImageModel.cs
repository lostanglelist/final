using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace logintest.Areas.Identity.Data
{
    public class ImageModel
    {

        [Key]
        public string ProductId { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string ProductName { get; set; }

        [Column(TypeName = "float")]
        public float ProductPrice { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string ProductDetail { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string ProductSlot { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Series { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Description { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string ProductSize { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string ProductRam { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string ProductGPU { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string ProductPOWER { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Image Name")]
        public string ProductImage { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
    }
}
