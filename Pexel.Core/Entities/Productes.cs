using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Core.Entities
{
    public class Productes
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Quantity * NewPrice;
        public string Description { get; set; }
        public int CategoryId { get; set; }

        //Navigation Property
        public Categories Category { get; set; }
        public List<Image> images { get; set; }
    }
}
