﻿using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace StoreApi.Security.Token
{
    public static class SignHandler
    {
        public static SecurityKey GetSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
