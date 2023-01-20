﻿namespace GamesAPI.Configurations
{
    public class JwtOptions
    {
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public string Secret { get; init; }

    }
}
