using System;

namespace RememBeer.Common.Cache
{
    public interface ICacheManager
    {
        double DefaultTimeoutInMinutes { get; }

        void Add(object toCache, string key);

        bool Exists(string key);

        void Remove(string key);

        T Get<T>(string key) where T : class;

        T Get<T>(string key, Func<T> fn) where T : class;

        T Get<T>(string key, double timeoutminutes, Func<T> fn) where T : class;
    }
}