using StoreApi.Domain.Interfaces;
using StoreApi.Domain.Responses;
using StoreApi.Security.Token;

namespace StoreApi.Services
{
    public class AuthenticationService:IAuthenticationService
    {
        private readonly ICustomerService customerService;
        private readonly ITokenHandler tokenHandler;

        public AuthenticationService(ICustomerService customerService, ITokenHandler tokenHandler)
        {
            this.customerService = customerService;
            this.tokenHandler = tokenHandler;
        }

        public AccessTokenResponse CreateAccessToken(string email, string password)
        {
            CustomerResponse customerResponse = customerService.GetCustomerWithEmailAndPassword(email, password);
            if (customerResponse.Success)
            {
                AccessToken accessToken = tokenHandler.CreateAccessToken(customerResponse.Customer);
                customerService.SaveRefreshToken(customerResponse.Customer.Id, accessToken.RefreshToken);
                return new AccessTokenResponse(accessToken);
            }
            else
            {
                return new AccessTokenResponse(customerResponse.Message);
            }
        }

        public AccessTokenResponse CreateAccessTokenByRefreshToken(string refreshToken)
        {
            CustomerResponse customerResponse = customerService.GetCustomerWithRefreshToken(refreshToken);

            if (customerResponse.Success)
            {
                if(customerResponse.Customer.RefreshTokenEndDate > DateTime.Now)
                {
                    AccessToken accessToken = tokenHandler.CreateAccessToken(customerResponse.Customer);
                    customerService.SaveRefreshToken(customerResponse.Customer.Id, accessToken.RefreshToken);
                    return new AccessTokenResponse(accessToken);
                }
                else
                {
                    return new AccessTokenResponse("Token Expire");
                }
            }
            else
            {
                return new AccessTokenResponse("RefreshToken not found");
            }
        }

        public AccessTokenResponse RevokeRefreshToken(string refreshToken)
        {
            CustomerResponse customerResponse = customerService.GetCustomerWithRefreshToken(refreshToken);
            if(customerResponse.Success)
            {
                customerService.RemoveRefreshToken(customerResponse.Customer);
                return new AccessTokenResponse(new AccessToken());
            }
            else
            {
                return new AccessTokenResponse("RefreshToken not found");
            }
        }
    }
}
