using AutoMapper;
using Kursa4.BLL.DTO;
using Kursa4.DAL.Entities;

namespace Kursa4.BLL.Profiles
{
    public class SubserviceProfile : Profile
    {
        public SubserviceProfile()
        {
            CreateMap<Subservice, SubserviceDTO>()
                .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => src.ServiceId))
                .ReverseMap()
                .ForMember(dest => dest.ServiceId, opt => opt.MapFrom(src => src.ServiceId))
                .ForMember(dest => dest.Service, opt => opt.Ignore());
        }
    }
}
