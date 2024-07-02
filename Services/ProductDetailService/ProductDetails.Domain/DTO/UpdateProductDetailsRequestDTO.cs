using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDetails.Domain.DTO
{
    public class UpdateProductDetailsRequestDTO
    {
        public string Id {  get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string Quantity {  get; set; }
    }
}
