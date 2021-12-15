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
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        private readonly ICarService _carService;
        private readonly IMapper _mapper;
        public RentalController(IRentalService rentalService, IMapper mapper, ICarService carService)
        {
            _rentalService = rentalService;      
            _mapper = mapper;
            _carService = carService;
        }

        [HttpPost("Rent")]
        public async Task<IActionResult> Rent([FromBody] RentalModel model)
        {
            var rental = _mapper.Map<Rental>(model);
            var car =await _carService.GetCarAsync(model.CarId);
            var result = await _rentalService.RentAsync(rental);
            if (result)
            {
                car.Available = false;
                await _carService.UpdateCarAsync(car);
                return Ok(result);
            }
            
            return BadRequest(false);
        }

        [HttpGet("GetAllRentals")]
        public async Task<IActionResult> GetAllRentals()
        {
            return Ok(await _rentalService.GetAllRentalsAsync());
        }

        [HttpPost("GetUnavailbleDates")]
        public async Task<IActionResult> GetUnavailbleDates([FromBody] int carId)
        {
            return Ok(await _rentalService.GetUnavailbleDates(carId));
        }


    }
}
