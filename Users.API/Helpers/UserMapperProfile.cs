using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Users.API.Helpers
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<Entities.User, Model.UserModel>();

            CreateMap<Model.UserForCreation, Entities.User>()
                .ForMember(dest => dest.WatchLaterId, opt => opt.MapFrom(x => Guid.NewGuid()));
        }
    }
}
