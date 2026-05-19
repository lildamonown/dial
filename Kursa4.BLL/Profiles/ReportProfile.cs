using AutoMapper;
using Kursa4.BLL.DTO;
using Kursa4.DAL.Entities;

namespace Kursa4.BLL.Profiles
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<Report, ReportDTO>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId));

            CreateMap<ReportDTO, Report>()
                .ForMember(dest => dest.Order, opt => opt.Ignore());
        }
    }
}
