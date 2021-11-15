using System;
using ReportSharp.Services.ApiAuthorizationService;

namespace ReportSharp.Builder.ApiOptionsBuilder
{
    public interface IApiOptionsBuilder
    {
        public IApiOptionsBuilder UseAuthorization<TAuthorizationService>()
            where TAuthorizationService : IApiAuthorizationService;

        public IApiOptionsBuilder UseAuthorization(Type authorizationServiceType);
    }
}