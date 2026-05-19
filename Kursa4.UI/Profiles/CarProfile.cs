using AutoMapper;
using Kursa4.BLL.DTO;
using Kursa4.UI.Models.Inputs;

namespace Kursa4.UI.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<CarDTO, CarForCreate>()
                .ReverseMap();
        }
    }
}
