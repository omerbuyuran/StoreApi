using StoreApi.Domain.Model;

namespace StoreApi.Security.Token
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(Customer customer);
        void RevokeRefreshToken(Customer customer);
    }
}
