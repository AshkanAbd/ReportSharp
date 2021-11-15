using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ReportSharp.Configs;

namespace ReportSharp.Services.ApiAuthorizationService
{
    public class DefaultApiAuthorizationService : IApiAuthorizationService
    {
        public DefaultApiAuthorizationService(IHttpContextAccessor contextAccessor, IOptions<ReportSharpConfig> options)
        {
            HttpContext = contextAccessor.HttpContext;
            ReportSharpConfig = options.Value;
        }

        public HttpContext HttpContext { get; }
        public ReportSharpConfig ReportSharpConfig { get; }

        public bool IsAuthorized()
        {
            var username = GetValueFromHeader("username");
            if (username == null) return false;

            var password = GetValueFromHeader("password");
            if (password == null) return false;

            if (username != ReportSharpConfig.Username) return false;

            var now = DateTime.Now;

            var usernameCode = GetAsciiValue(username);
            var secretKeyCode = GetAsciiValue(ReportSharpConfig.SecretKey);
            var mergedUsernameSecretKey = long.Parse($"{usernameCode}{secretKeyCode}");

            var todayCode = long.Parse($"{now.Year:0000}{now.Month:00}{now.Day:00}");

            var validPassword = mergedUsernameSecretKey ^ todayCode;

            return validPassword.ToString() == password;
        }

        private long GetAsciiValue(string str)
        {
            return str.ToCharArray().Aggregate(0, (current, c) => current + c);
        }

        private string GetValueFromHeader(string key)
        {
            return HttpContext.Request.Headers
                .Where(x => x.Key == key)
                .Select(x => x.Value.ToString())
                .FirstOrDefault();
        }
    }
}