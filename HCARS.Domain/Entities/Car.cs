using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCARS.Domain.Entities
{
    public class Car
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public bool Available { get; set; }
        public virtual Brand? Brand { get; set; }
        public int BrandId { get; set; }
        public string Model { get; set; }
        public decimal PricePerDay { get; set; }
        public string ImageUrl { get; set; }
        public Location? Location { get; set; }
        public int? LocationId { get; set; }
        public int NumberOfDoors { get; set; }
        public string Color { get; set; }
        public int Mileage { get; set; }
        public int Power { get; set; }


    }
}
