﻿using Microsoft.EntityFrameworkCore;

namespace SurveyBasket.ApI.Entities
{
    [Owned]
    public class RefreshTokens
    {
        public string Token { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresOn { get; set; }
        public DateTime? RevokedOn { get; set; }

        public bool IsExpired => DateTime.UtcNow >= ExpiresOn;

        public bool IsActive => RevokedOn is null && !IsExpired;

    }

}