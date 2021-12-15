using HCARS.Domain.Consts;
using HCARS.Domain.Entities;
using HCARS.Domain.IRepository;
using HCARS.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCARS.Services.Services
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Car> AddCarAsync(Car car)
        {
           await _unitOfWork.Cars.AddAsync(car);
            await _unitOfWork.CompleteAsync();
            return await GetCarAsync(car.Id);
        }

        public async Task<IEnumerable<Car>> AddRangeOfCars(IEnumerable<Car> cars)
        {
            await _unitOfWork.Cars.AddRangeAsync(cars);
            return cars;
        }

        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            return await _unitOfWork.Cars.GetAllAsync(new[] { "Brand" });
        }

        public Task<IEnumerable<Car>> SearchCarsOrderedByID(string name, int? skip, int? take)
        {
            throw new NotImplementedException();
        }

        public async Task<Car> GetCarAsync(int id)
        {
            return await _unitOfWork.Cars.FindAsync(c => c.Id == id, new[] { "Brand"});
        }

        public async Task<Car> UpdateCarAsync(Car car)
        {
             _unitOfWork.Cars.Update(car);
             _unitOfWork.Complete();
            return await GetCarAsync(car.Id);
        }

        public int DeleteCar(int id)
        {
            _unitOfWork.Cars.Delete(id);
           return _unitOfWork.Complete();
        }

        public async Task UpdateCarImageAsync(int id, string url)
        {
            var car = await GetCarAsync(id);
            car.ImageUrl = url;
            _unitOfWork.Cars.Update(car);
            _unitOfWork.Complete();
        }

        public async Task<IEnumerable<Car>> GetAllAvailbleCarsAsync()
        {
            return await _unitOfWork.Cars.FindAllAsync(c => c.Available == true ,new[] { "Brand" });
        }
    }
}
