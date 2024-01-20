using StoreApi.Domain.Responses;

namespace StoreApi.Domain.Interfaces
{
    public interface IAuthenticationService
    {
        AccessTokenResponse CreateAccessToken(string email,string password);
        AccessTokenResponse CreateAccessTokenByRefreshToken(string refreshToken);
        AccessTokenResponse RevokeRefreshToken(string refreshToken);
    }
}
