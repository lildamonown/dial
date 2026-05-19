using AutoMapper;
using Kursa4.BLL.DTO;
using Kursa4.UI.Models.Inputs;
using Kursa4.UI.Models.Outputs;

namespace Kursa4.UI.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderForCreate, OrderDTO>()
                .ForMember(dest => dest.CarId, opt => opt.MapFrom(scr => scr.Car.Id));

            CreateMap<OrderForEdit, OrderDTO>()
            .ForMember(dest => dest.Subservices,
                      opt => opt.MapFrom(src => src.Subservices));

            CreateMap<OrderDTO, OrderForEdit>()
                .ForMember(dest => dest.Subservices,
                          opt => opt.MapFrom(src => src.Subservices));

            CreateMap<OrderDTO, OrderModel>();

            CreateMap<OrderDTO, OrderModel>()
                .ForMember(dest => dest.UserName, opt => opt.Ignore())
                .ForMember(dest => dest.UserSurname, opt => opt.Ignore())
                .ForMember(dest => dest.UserPhoneNumber, opt => opt.Ignore())
                .ForMember(dest => dest.StatusName, opt => opt.Ignore())
                .ForMember(dest => dest.NameSubservice, opt => opt.MapFrom(src => src.Subservices.Select(s => s.Name).ToList()))
                .ForMember(dest => dest.SumPrice, opt => opt.MapFrom(src => src.Subservices.Sum(s => s.Price)));
        }
    }
}
