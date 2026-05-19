using AutoMapper;
using Kursa4.BLL.DTO;
using Kursa4.DAL.Entities;

namespace Kursa4.BLL.Profiles
{
    public class CarSeriesProfile : Profile
    {
        public CarSeriesProfile()
        {
            CreateMap<CarSeries, CarSeriesDTO>().ReverseMap();
        }
    }
}
