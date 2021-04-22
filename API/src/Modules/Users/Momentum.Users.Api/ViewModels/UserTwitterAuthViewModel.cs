﻿using System;
using System.Text.Json.Serialization;

namespace Momentum.Users.Api.ViewModels
{
    public class UserTwitterAuthViewModel
    {
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("id")]
        public Guid UserId { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
    }
}