using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreApi.Domain.Interfaces;
using StoreApi.Domain.Request;
using StoreApi.Domain.Responses;
using StoreApi.Extensions;

namespace StoreApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IMapper mapper;
        public LoginController(IAuthenticationService authenticationService, IMapper mapper)
        {
            this.authenticationService = authenticationService;
            this.mapper = mapper;
        }
        [HttpPost]
        public IActionResult AccessToken(LoginRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            else
            {
                AccessTokenResponse accessTokenResponse = authenticationService.CreateAccessToken(request.email, request.password);
                if(accessTokenResponse.Success)
                {
                    return Ok(accessTokenResponse.accessToken);
                }
                else
                {
                    return BadRequest(accessTokenResponse.Message);
                }
            }
        }

        [HttpPost]
        public IActionResult RefreshToken(TokenRequest request)
        {
            AccessTokenResponse accessTokenResponse = authenticationService.CreateAccessTokenByRefreshToken(request.refreshToken);
            if (accessTokenResponse.Success)
            {
                return Ok(accessTokenResponse.accessToken);
            }
            else
            {
                return BadRequest(accessTokenResponse.Message);
            }
        }

        [HttpPost]
        public IActionResult RemoveRefreshToken(TokenRequest request)
        {
            AccessTokenResponse accessTokenResponse = authenticationService.RevokeRefreshToken(request.refreshToken);
            if (accessTokenResponse.Success)
            {
                return Ok(accessTokenResponse.accessToken);
            }
            else
            {
                return BadRequest(accessTokenResponse.Message);
            }
        }
    }
}
