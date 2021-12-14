using AutoMapper;
using HCARS.Domain.Entities;
using HCARS.Domain.EntitiesModels;
using HCARS.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HCARS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
       
        private readonly IMapper _mapper;
        public CarsController(ICarService carService, IWebHostEnvironment environment, IMapper mapper)
        {
            _carService = carService;
            _mapper = mapper;
        }

        [HttpGet("GetCar")]
        public async Task<IActionResult> GetById()
        {
            throw new Exception("Text Logging");
            return Ok(await _carService.GetCarAsync(1));
        }


        [HttpGet("GetAllCars")]
        public async Task<IActionResult> GetCars()
        {
            return Ok(await _carService.GetAllCarsAsync());
        }

        [HttpGet("GetOrdered")]
        public async Task<IActionResult> GetOrdered()
        {
            return Ok(await _carService.SearchCarsOrderedByID(" ",1,1));
        }

        [HttpPost("AddCar")]
        public async Task<IActionResult> AddCar([FromBody] Car car)
        {
            return Ok(await _carService.AddCarAsync(car));
        }

        [HttpPut("UpdateCar")]
        public async Task<IActionResult> UpdateCar([FromBody] Car car)
        {
            return Ok(await _carService.UpdateCarAsync(car));
        }

        [HttpPost("DeleteCar")]
        public IActionResult DeleteCar([FromBody] int id)
        {
            return Ok(_carService.DeleteCar(id));
        }
    }
}
