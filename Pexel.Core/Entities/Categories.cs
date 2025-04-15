using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Core.Entities
{
    public class Categories
    { 
        public int Id { get; set; }
        public string CategoryName{ get; set; }

        public List<Product> products { get; set; }
    }
}
