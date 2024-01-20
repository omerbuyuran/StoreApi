using StoreApi.Security.Token;

namespace StoreApi.Domain.Responses
{
    public class AccessTokenResponse:BaseResponse
    {
        public AccessToken accessToken { get; set; }

        private AccessTokenResponse(bool success,string message,AccessToken accessToken) : base(success, message)
        {
            this.accessToken = accessToken;
        }

        public AccessTokenResponse(AccessToken accessToken):this(true,string.Empty,accessToken) { }

        public AccessTokenResponse(string message) : this(false, message, null) { }
    }
}
