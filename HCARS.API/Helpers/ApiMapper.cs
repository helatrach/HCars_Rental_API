using AutoMapper;
using HCARS.Domain.Entities;
using HCARS.Domain.EntitiesModels;

namespace HCARS.API.Helpers
{
    public class ApiMapper : Profile
    {
        public ApiMapper()
        {
            CreateMap<Car,CarModel>().ReverseMap();
        }
    }
}
