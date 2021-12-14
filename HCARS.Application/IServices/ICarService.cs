using HCARS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCARS.Services.IServices
{
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetAllCarsAsync();
        Task<Car> GetCarAsync(int id);
        Task<IEnumerable<Car>> SearchCarsOrderedByID(string name, int? skip , int? take);
        Task<Car> AddCarAsync (Car car);
        Task<IEnumerable<Car>> AddRangeOfCars(IEnumerable<Car> cars);
        Task<Car> UpdateCarAsync(Car car);

        int DeleteCar(int id);
        Task UpdateCarImageAsync(int id, string url);
    }
}
