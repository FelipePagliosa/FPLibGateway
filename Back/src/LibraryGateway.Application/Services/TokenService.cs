using System.Text;
using System.Security.Claims;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using LibraryGateway.Application.Interfaces;
using LibraryGateway.Application.Requests.UserRequests;
using LibraryGateway.Domain.Models;
using System.IdentityModel.Tokens.Jwt;

namespace LibraryGateway.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        private readonly IMapper _mapper;
        public readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("test super secret mega hyper omega key for authentication"));
        }

        public async Task<string> CreateToken(User user)
        {

            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Perfil.ToString())
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var date = DateTime.Now;
            var timeoutSession = Convert.ToInt32(this._config.GetSection("TimeoutSession").Exists() ? this._config.GetSection("TimeoutSession").Value : "60");

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.Now.AddMinutes(timeoutSession),
                NotBefore = date,
                SigningCredentials = creds
            };


            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);



        }
    }
}