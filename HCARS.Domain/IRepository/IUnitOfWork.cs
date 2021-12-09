using HCARS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCARS.Domain.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICarsRepository Cars { get; }
        IBaseRepository<Brand> Brands { get; }
        Task<int> CompleteAsync();
        int Complete();
    }
}
