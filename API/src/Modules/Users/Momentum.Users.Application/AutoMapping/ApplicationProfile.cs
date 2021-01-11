﻿using AutoMapper;
using Momentum.Users.Application.DTOs;
using Momentum.Users.Core.Models;

namespace Momentum.Users.Application.AutoMapping
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            
            CreateMap<Roles, RolesDto>();
            CreateMap<RolesDto, Roles>();
            
            CreateMap<Bans, BansDto>();
            CreateMap<BansDto, Bans>();

            CreateMap<UserProfile, UserProfileDto>();
            CreateMap<UserProfileDto, UserProfile>();
        }
    }
}