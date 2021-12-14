using HCARS.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCARS.Infrastructure.Context
{
    public class HCarsDbContext : IdentityDbContext
    {
        public HCarsDbContext(DbContextOptions<HCarsDbContext> options) : base (options)
        {

        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Rental> Rentals { get; set; }
    }
}
