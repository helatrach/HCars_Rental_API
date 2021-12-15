using HCARS.Domain.Entities;
using HCARS.Domain.IRepository;
using HCARS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCARS.Services.Services
{
    public class RentalService : IRentalService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RentalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Rental>> GetAllRentalsAsync()
        {
            return await _unitOfWork.Rentals.GetAllAsync();
        }

        public async Task<IEnumerable<string>> GetUnavailbleDates(int CarId)
        {
            List<string> dates = new List<string>();
            var result = await _unitOfWork.Rentals.FindAllAsync(c => c.CarId == CarId & c.ReturnDate >= DateTime.UtcNow, new[] { "Car" });

            foreach (var rental in result)
            {
                for (var dt = rental.RentDate; dt <= rental.ReturnDate; dt = dt.AddDays(1))
                {
                    dates.Add(dt.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
                }
            }
            return dates;
        }

        public async Task<bool> RentAsync(Rental rental)
        {
            await _unitOfWork.Rentals.AddAsync(rental);
           var result =  await _unitOfWork.CompleteAsync();
            if(result> 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}
