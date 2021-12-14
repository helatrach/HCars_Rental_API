using HCARS.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCARS.Domain.EntitiesModels
{
    public class CarModel
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public bool Available { get; set; }
        public int BrandId { get; set; }
        public string Model { get; set; }
        public decimal PricePerDay { get; set; }
        public string ImageUrl { get; set; }
        public int NumberOfDoors { get; set; }
        public string Color { get; set; }
        public int Mileage { get; set; }
        public int Power { get; set; }
        public IFormFile files { get; set; }
    }
}
