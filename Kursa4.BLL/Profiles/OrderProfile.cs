using AutoMapper;
using Kursa4.BLL.DTO;
using Kursa4.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursa4.BLL.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.CarId, opt => opt.MapFrom(src => src.CarId))
                .ReverseMap()
                        .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                        .ForMember(dest => dest.User, opt => opt.Ignore())
                        .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                        .ForMember(dest => dest.Status, opt => opt.Ignore())
                        .ForMember(dest => dest.CarId, opt => opt.MapFrom(src => src.CarId))
                        .ForMember(dest => dest.Car, opt => opt.Ignore());
        }
    }
}
