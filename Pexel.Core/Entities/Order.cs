using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public string CustomerEmail { get; set; }
        public decimal SupTotal { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public CustomerAddress customerAddress { get; set; }

        public DeliverMethod deliveryMethod { get; set; }

        public IReadOnlyList<OrderItem> orderItems { get; set; }
    }
}
