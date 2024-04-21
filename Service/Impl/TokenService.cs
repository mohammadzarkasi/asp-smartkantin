using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using smartkantin.Models;

namespace smartkantin.Service.Impl
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration config;
        private readonly SymmetricSecurityKey key;
        private readonly string Issuer, Audience;
        public TokenService(IConfiguration config)
        {
            this.config = config;
            var signingKey = GetString(config, "JWT:SigningKey", "wkwkwkwk");
            Issuer = GetString(config, "JWT:Issuer", "http://localhost");
            Audience = GetString(config, "JWT:Audience", "http://localhost");
            key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey));
        }

        private string GetString(IConfiguration config, string key, string defaultValue)
        {
            var skey = config[key];
            if (skey != null)
            {
                return skey;
            }
            return defaultValue;
        }

        public string CreateToken(AppUser user)
        {
            Console.WriteLine("create claims");
            var claims = new List<Claim>
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email, user.Email ?? ""),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.GivenName, user.UserName ?? ""),
                new Claim("user_id", user.Id),
            };

            Console.WriteLine("create creds");
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            Console.WriteLine("create token descriptor");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = Issuer,
                Audience = Audience,
            };

            Console.WriteLine("create token handler");
            var tokenHandler = new JwtSecurityTokenHandler();

            Console.WriteLine("create token");
            var token = tokenHandler.CreateToken(tokenDescriptor);

            Console.WriteLine("write token");
            return tokenHandler.WriteToken(token);
        }
    }
}