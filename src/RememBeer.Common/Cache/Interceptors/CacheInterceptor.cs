using System;
using System.Text;

using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Request;

namespace RememBeer.Common.Cache.Interceptors
{
    public class CacheInterceptor : IInterceptor
    {
        private readonly ICacheManager cache;

        public CacheInterceptor(ICacheManager cache)
        {
            if (cache == null)
            {
                throw new ArgumentNullException(nameof(cache));
            }

            this.cache = cache;
        }

        public TimeSpan? Timeout { get; set; }

        public void Intercept(IInvocation invocation)
        {
            var timeOut = this.cache.DefaultTimeoutInMinutes;
            if (this.Timeout.HasValue)
            {
                timeOut = this.Timeout.Value.TotalMinutes;
            }

            var cacheKey = this.GetCacheKey(invocation.Request);

            invocation.ReturnValue = this.cache.Get(cacheKey,
                                                    timeOut,
                                                    () =>
                                                    {
                                                        invocation.Proceed();
                                                        return invocation.ReturnValue;
                                                    });
        }

        private string GetCacheKey(IProxyRequest request)
        {
            var sb = new StringBuilder();
            sb.Append(request.Target.GetType().FullName);
            sb.Append(".");
            sb.Append(request.Method.Name);
            sb.Append(".");

            foreach (var argument in request.Arguments)
            {
                if (argument is string)
                {
                    sb.Append((string)argument);
                }
                else
                {
                    sb.Append(argument.GetHashCode());
                }

                sb.Append(".");
            }

            return sb.ToString();
        }
    }
}
