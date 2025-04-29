using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Core.DTOs.Orders
{
    public record OrderDto
    {
        public int deliveryId { get; set; }
        public string basketId { get; set; }
        public CustomerAddressDto customerAddress { get; set; }
    }

    public record CustomerAddressDto 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
    } 
}
