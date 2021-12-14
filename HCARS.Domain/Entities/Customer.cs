using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCARS.Domain.Entities
{
    public class Customer 
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty ;
        public string Email { get; set; } = string.Empty;
        public DateTime  DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
    }
}
