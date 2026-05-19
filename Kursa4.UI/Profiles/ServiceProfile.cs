using AutoMapper;
using Kursa4.BLL.DTO;
using Kursa4.UI.Models.Inputs;
using Kursa4.UI.Models.Outputs;

namespace Kursa4.UI.Profiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<ServiceDTO, ServiceModel>()
                .ForMember(dest => dest.Subservices, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ServiceDTO, ServiceForEdit>()
                .ReverseMap();

            CreateMap<ServiceDTO, ServiceForCreate>()
                .ReverseMap();
        }
    }
}
