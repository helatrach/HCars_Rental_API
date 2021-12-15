using HCARS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCARS.Services.IServices
{
    public interface IRentalService
    {
        Task<bool> RentAsync(Rental rental);
        Task<IEnumerable<Rental>> GetAllRentalsAsync();
        Task<IEnumerable<string>> GetUnavailbleDates(int CarId);
    }
}
