using AutoMapper;
using Kursa4.BLL.DTO;
using Kursa4.UI.Models.Inputs;
using Kursa4.UI.Models.Outputs;

namespace Kursa4.UI.Profiles
{
    public class ReportProfile : Profile
    {
        public ReportProfile()
        {
            CreateMap<ReportDTO, ReportModel>();

            CreateMap<ReportDTO, ReportForCreate>()
                .ReverseMap();
        }
    }
}
