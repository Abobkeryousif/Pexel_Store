using Pexel.Core.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Core.Entities
{
    public class Order
    {
        public Order()
        {
        }
        public Order(string customerEmail, decimal supTotal, CustomerAddress customerAddress, DeliveryMethod deliveryMethod, IReadOnlyList<OrderItem> orderItems)
        {
           
            CustomerEmail = customerEmail;
            SupTotal = supTotal;
            this.customerAddress = customerAddress;
            this.deliveryMethod = deliveryMethod;
            this.orderItems = orderItems;
        }

        public int Id { get; set; }
         public string CustomerEmail { get; set; }
        public decimal SupTotal { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public CustomerAddress customerAddress { get; set; }
        public DeliveryMethod deliveryMethod { get; set; }
        public IReadOnlyList<OrderItem> orderItems { get; set; }
        public OrderStatues orderStatues { get; set; } = OrderStatues.Pending;
        public decimal GetTotal() =>
            SupTotal + deliveryMethod.Price;
        
    }
}
