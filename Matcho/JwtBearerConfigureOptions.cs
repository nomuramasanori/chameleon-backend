using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Matcho
{
    public class JwtBearerConfigureOptions : IConfigureNamedOptions<JwtBearerOptions>
    {
        public void Configure(string name, JwtBearerOptions options)
        {
            if (name != JwtBearerDefaults.AuthenticationScheme)
            {
                return;
            }

            options.TokenValidationParameters = new TokenValidationParameters
            {
                AudienceValidator = this.AudienceValidatorDelegate,
                ValidIssuer = "ExJwtAuth",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(new String('s', 128))),
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
            };
        }

        public void Configure(JwtBearerOptions options)
        {
            Configure(JwtBearerDefaults.AuthenticationScheme, options);
        }

        public bool AudienceValidatorDelegate(IEnumerable<string> audiences, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            return true;
        }
    }
}