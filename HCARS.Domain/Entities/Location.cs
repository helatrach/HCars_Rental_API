using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCARS.Domain.Entities
{
    public class Location
    {
        public int id { get; set; }
        public string City { get; set; }
        public string Address  { get; set; }
        public int PostalCode { get; set; }
    }
}
