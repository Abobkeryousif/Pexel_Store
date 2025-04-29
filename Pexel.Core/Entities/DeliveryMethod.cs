using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Core.Entities
{
    public class DeliveryMethod
    {
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod( string companyName, string description, string deliveryTime, decimal price)
        {
         
            CompanyName = companyName;
            Description = description;
            DeliveryTime = deliveryTime;
            Price = price;
        }
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }

        public string DeliveryTime { get; set; }
        public decimal Price { get; set; }

    }
}
