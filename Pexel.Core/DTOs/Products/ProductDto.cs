using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Core.DTOs.Product
{
    public class ProductDto
    {
        public string Name { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Quantity * NewPrice;
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}
