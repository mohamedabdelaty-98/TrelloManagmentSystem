using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrelloManagmentSystem.Models;

namespace SurveyBasket.ApI.JwtService
{
    public class JwtProvider : IJwtProvider
    {
        private readonly SymmetricSecurityKey _key;
        private readonly IConfiguration _configuration;

        public JwtProvider(  IConfiguration configuration)
        {
            
            _configuration = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:key"]));
        }

        public async Task<(string token, DateTime expireIn)> CreateTokenAsync(AppUser user)
        {
            Claim[] claims =
                [
                    new(JwtRegisteredClaimNames.Sub, user.Id),
                    new(JwtRegisteredClaimNames.Email, user.Email!),
                    new(JwtRegisteredClaimNames.GivenName, user.FirstName),
                    new(JwtRegisteredClaimNames.FamilyName, user.LastName),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                
                ];

            DateTime expiresTime = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Token:ExpireMinutes"]));


            var token = new JwtSecurityToken
            (
                claims: claims,
                issuer: _configuration["Token:ValidIssuer"],
                audience: _configuration["Token:ValidAudiance"] ,
                signingCredentials: new SigningCredentials(_key, SecurityAlgorithms.HmacSha256),
                expires : expiresTime
            );

            return (token: new JwtSecurityTokenHandler().WriteToken(token), expireIn: expiresTime );
        }

        public  string? ValidatToken(string token)
        {
              var tokenHandler =   new JwtSecurityTokenHandler();


            try
            {
                 tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    IssuerSigningKey = _key,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero
                },  out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                return   jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
         
            }
         
            catch 
            {
                return null;
            }

        }
    }

}
