using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StoreApi.Domain.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace StoreApi.Security.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly TokenOptions tokenOptions;
        public TokenHandler(IOptions<TokenOptions> tokenOptions) 
        {
            this.tokenOptions = tokenOptions.Value;
        }
        public AccessToken CreateAccessToken(Customer customer)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration);

            var securityKey = SignHandler.GetSecurityKey(tokenOptions.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer:tokenOptions.Issuer,
                audience:tokenOptions.Audience,
                expires:accessTokenExpiration,
                notBefore:DateTime.Now,
                claims:GetClaim(customer),
                signingCredentials: signingCredentials
                );

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(securityToken);

            AccessToken accessToken = new AccessToken();
            accessToken.Token = token;
            accessToken.RefreshToken = CreateRefreshToken();
            accessToken.Expiration = accessTokenExpiration;
            return accessToken;
        }

        private IEnumerable<Claim> GetClaim(Customer customer)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, customer.Email),
                new Claim(ClaimTypes.Name, $"{customer.Name} {customer.Surname}"),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            return claims;
        }

        private string CreateRefreshToken()
        {
            var numberByte = new Byte[32];
            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(numberByte);

                return Convert.ToBase64String(numberByte);
            }
        }

        public void RevokeRefreshToken(Customer customer)
        {
            customer.RefreshToken = null;
        }
    }
}
