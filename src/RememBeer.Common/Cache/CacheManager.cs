using System;
using System.Web;

namespace RememBeer.Common.Cache
{
    public class CacheManager : ICacheManager
    {
        private readonly object locker = new object();

        public CacheManager(double defaultTimeoutInMinutes)
        {
            this.DefaultTimeoutInMinutes = defaultTimeoutInMinutes;
        }

        public double DefaultTimeoutInMinutes { get; }

        public void Add(object toCache, string key)
        {
            Add(toCache, key, this.DefaultTimeoutInMinutes);
        }

        public bool Exists(string key)
        {
            return HttpContext.Current.Cache[key] != null;
        }

        public void Remove(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }

        public T Get<T>(string key) where T : class
        {
            return HttpContext.Current.Cache[key] as T;
        }

        public T Get<T>(string key, Func<T> fn) where T : class
        {
            return this.Get(key, this.DefaultTimeoutInMinutes, fn);
        }

        public T Get<T>(string key, double timeoutminutes, Func<T> fn) where T : class
        {
            var obj = this.Get<T>(key);
            if (obj != default(T))
            {
                return obj;
            }

            lock (this.locker)
            {
                obj = this.Get<T>(key);
                if (obj != default(T))
                {
                    return obj;
                }

                obj = fn();
                Add(obj, key, timeoutminutes);
            }

            return obj;
        }

        private static void Add(object toCache, string key, double cacheTimeout)
        {
            HttpContext.Current.Cache.Insert(key,
                                             toCache,
                                             null,
                                             DateTime.UtcNow.AddMinutes(cacheTimeout),
                                             System.Web.Caching.Cache.NoSlidingExpiration);
        }
    }
}
