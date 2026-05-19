using AutoMapper;
using Kursa4.BLL.DTO;
using Kursa4.DAL.Entities;

namespace Kursa4.BLL.Profiles
{
    public class CarBrandProfile : Profile
    {
        public CarBrandProfile()
        {
            CreateMap<CarBrand, CarBrandDTO>().ReverseMap();
        }
    }
}
