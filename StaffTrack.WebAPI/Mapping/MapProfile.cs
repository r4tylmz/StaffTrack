using System.Collections.Generic;
using AutoMapper;
using StaffTrack.Core.Entities;
using StaffTrack.WebAPI.DTOs;

namespace StaffTrack.WebAPI.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Constant, ConstantDto>();
            CreateMap<ConstantDto, Constant>();
        }
    }
}