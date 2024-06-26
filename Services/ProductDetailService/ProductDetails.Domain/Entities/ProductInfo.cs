using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDetails.Domain.Entities
{
    public class ProductInfo
    {
        [Key]
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string  Design {  get; set; }
        public decimal Quantity { get; set; }
        public bool isDelete{ get; set; } = false;

        public ProductInfo()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.UtcNow;
        }
    }
}
