using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pexel.Core.Entities
{
    public class CustomerAddress
    {
        public CustomerAddress()
        {
            
        }
        public CustomerAddress(int id, string firstName, string lastName, string region, string city, string street, string zipCode)
         {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Region = region;
            City = city;
            Street = street;
            ZipCode = zipCode;
        }

        public int Id { get; set; }
        public string FirstName{ get; set; }
        public string LastName{ get; set; }
        public string Region{ get; set; }
        public string City{ get; set; }
        public string Street{ get; set; }
        public string ZipCode{ get; set; }
    }
}
