using System;

using Ninject;
using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Attributes;
using Ninject.Extensions.Interception.Request;

using RememBeer.Common.Cache.Interceptors;

namespace RememBeer.Common.Cache.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheAttribute : InterceptAttribute
    {
        public const int DefaultTimeoutInMinutes = 10;

        public CacheAttribute()
        {
            this.TimeoutInMinutes = DefaultTimeoutInMinutes;
        }

        public CacheAttribute(int timeoutInMinutes)
        {
            if (timeoutInMinutes < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(timeoutInMinutes), "Cache timeout must be a positive number!");
            }

            this.TimeoutInMinutes = timeoutInMinutes;
        }

        public int TimeoutInMinutes { get; }

        public override IInterceptor CreateInterceptor(IProxyRequest request)
        {
            var interceptor = request.Kernel.Get<CacheInterceptor>();

            if (this.TimeoutInMinutes > 0)
            {
                interceptor.Timeout = TimeSpan.FromMinutes(this.TimeoutInMinutes);
            }

            return interceptor;
        }
    }
}
