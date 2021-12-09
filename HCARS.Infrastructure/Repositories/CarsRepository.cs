

using HCARS.Domain.Entities;
using HCARS.Domain.IRepository;
using HCARS.Infrastructure.Context;

namespace HCARS.Infrastructure.Repositories
{
    public class CarsRepository : BaseRepository<Car>, ICarsRepository
    {
        private readonly HCarsDbContext _context;
        public CarsRepository(HCarsDbContext context): base(context)
        {

        }
        public IEnumerable<Car> SpecialMethod()
        {
            throw new NotImplementedException();
        }
    }
}
