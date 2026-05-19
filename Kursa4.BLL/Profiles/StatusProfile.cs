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
    public class StatusProfile: Profile
    {
        public StatusProfile()
        {
            CreateMap<Status,StatusDTO>().ReverseMap();
        }

    }
}
