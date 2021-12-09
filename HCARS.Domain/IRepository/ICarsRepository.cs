using HCARS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCARS.Domain.IRepository
{
    public interface ICarsRepository : IBaseRepository<Car>
    {
        IEnumerable<Car> SpecialMethod();
    }
}
