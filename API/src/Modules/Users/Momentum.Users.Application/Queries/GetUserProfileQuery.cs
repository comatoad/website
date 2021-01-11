﻿using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Momentum.Users.Application.DTOs;
using Momentum.Users.Core.Repositories;

namespace Momentum.Users.Application.Queries
{
    public class GetUserProfileQuery : IRequest<UserProfileDto>
    {
        public Guid UserId { get; set; }
    }
    
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileDto>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMapper _mapper;

        public GetUserProfileQueryHandler(IUserProfileRepository userProfileRepository, IMapper mapper)
        {
            _userProfileRepository = userProfileRepository;
            _mapper = mapper;
        }

        public async Task<UserProfileDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var userProfile = await _userProfileRepository.GetByUserId(request.UserId);

            return _mapper.Map<UserProfileDto>(userProfile);
        }
    }
}