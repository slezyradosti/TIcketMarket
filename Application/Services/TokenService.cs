using Application.DTOs.Users.DTOS;
using Domain.Models.Tables;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services
{
    public class TokenService
    {
        private readonly IOptions<Security> _config;

        public TokenService(IOptions<Security> config)
        {
            _config = config;
        }

        public void CreateClaims(ApplicationUser user, out List<Claim> claims)
        {
            claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };
        }

        public void AddClaims(ref List<Claim> claims, ApplicationUser user)
        {
            claims.AddRange(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                }
            );
        }

        public string CreateToken(ApplicationUser user, IList<Claim> claims)
        {
            //CreateClaims(user, out var claims);

            var convClaims = claims.ToList();
            AddClaims(ref convClaims, user); //add extra claims for jwt token

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Value.SymmetricSecurityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            CreateTokenDescriptor(convClaims, credentials, out var tokenDescriptor);

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public void CreateTokenDescriptor(List<Claim> claims, SigningCredentials credentials, out SecurityTokenDescriptor tokenDescriptor)
        {
            tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = credentials
            };
        }
    }
}
