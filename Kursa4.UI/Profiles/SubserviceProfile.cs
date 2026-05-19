using AutoMapper;
using Kursa4.BLL.DTO;
using Kursa4.UI.Models.Inputs;
using Kursa4.UI.Models.Outputs;

namespace Kursa4.UI.Profiles
{
    public class SubserviceProfile : Profile
    {
        public SubserviceProfile()
        {
            CreateMap<SubserviceDTO, SubserviceModel>()
                .ReverseMap();

            CreateMap<SubserviceDTO, SubserviceForEdit>()
                .ReverseMap();

            CreateMap<SubserviceDTO, SubserviceForCreate>()
                .ReverseMap();
        }
    }
}
