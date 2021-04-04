﻿using AutoMapper;
using Momentum.Users.Api.ViewModels;
using Momentum.Users.Application.DTOs;
using Momentum.Users.Application.DTOs.Auth;

namespace Momentum.Users.Api.AutoMapping
{
    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<UserDto, UserViewModel>();
            CreateMap<ProfileDto, UserProfileViewModel>();
            CreateMap<StatsDto, UserStatsViewModel>();
            
            CreateMap<UserTwitterDto, UserTwitterAuthViewModel>();
            CreateMap<UserTwitchDto, UserTwitchAuthViewModel>();
            CreateMap<UserDiscordDto, UserDiscordAuthViewModel>();
        }
    }
}