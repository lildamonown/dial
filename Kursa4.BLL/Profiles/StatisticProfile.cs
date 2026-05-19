using AutoMapper;
using Kursa4.BLL.DTO;
using Kursa4.DAL.Entities;

namespace Kursa4.BLL.Profiles
{
    public class StatisticProfile : Profile
    {
        public StatisticProfile()
        {
            CreateMap<Statistic, StatisticDTO>().ReverseMap();
        }

    }
}
