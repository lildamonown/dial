using AutoMapper;
using Kursa4.DAL.Entities;
using Kursa4.UI.Models.Inputs;
using Kursa4.UI.Models.Outputs;
using Microsoft.AspNetCore.Identity;

namespace Kursa4.UI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegister, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName));

            CreateMap<User, UserForDisplay>()
                .ForMember(dest => dest.Role, opt => opt.Ignore()); 
        }
    }
}
