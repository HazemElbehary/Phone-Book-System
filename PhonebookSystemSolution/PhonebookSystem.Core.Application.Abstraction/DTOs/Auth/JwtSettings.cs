﻿namespace PhonebookSystem.Core.Application.Abstraction.DTOs.Auth
{
    public class JwtSettings
    {
        public required string Key { get; set; }
        public required string Audience { get; set; }
        public required string Issure { get; set; }
        public double DurationInMinutes { get; set; }
    }
}
