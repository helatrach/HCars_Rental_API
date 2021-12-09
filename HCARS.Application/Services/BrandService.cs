using HCARS.Domain.Entities;
using HCARS.Domain.IRepository;
using HCARS.Services.IServices;

namespace HCARS.Services.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            return await _unitOfWork.Brands.GetAllAsync();
        }
    }
}
