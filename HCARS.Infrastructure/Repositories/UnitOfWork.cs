using HCARS.Domain.Entities;
using HCARS.Domain.IRepository;
using HCARS.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCARS.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HCarsDbContext _context;
        public ICarsRepository Cars { get; private set; }

        public IBaseRepository<Brand> Brands { get; private set; }
        public IBaseRepository<Rental> Rentals { get; private set; }

        public UnitOfWork(HCarsDbContext context)
        {
            _context = context;
            Cars = new CarsRepository(_context);
            Brands = new BaseRepository<Brand>(_context);
            Rentals = new BaseRepository<Rental>(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public int Complete ()
        {
            return  _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
