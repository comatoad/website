﻿using System;
using Momentum.Framework.Core.Models;

namespace Momentum.Users.Core.Models
{
    public class UserProfile : TimeTrackedModel
    {
        public Guid UserId { get; set; }
        public string Bio { get; set; }
    }
}