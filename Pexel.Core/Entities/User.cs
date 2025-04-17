

namespace Pexel.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName => $"{FirstName} {LastName}";
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
    }
}
