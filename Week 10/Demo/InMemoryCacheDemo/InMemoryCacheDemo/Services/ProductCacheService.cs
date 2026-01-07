using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;

namespace InMemoryCacheDemo.Services
{
   /// <summary>
   /// This is products controller
   /// </summary>
    public class ProductCacheService
    {
        private static readonly MemoryCache _cache = MemoryCache.Default;
        public static object GetProducts()
        {
            var cacheKey = "PRODUCT_LIST";

            if (_cache.Contains(cacheKey))
                return _cache.Get(cacheKey);

            var products = new
            {
                Source = "Database",
                Time = DateTime.UtcNow
            };

            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(30)
            };

            _cache.Set(cacheKey, products, policy);

            return products;
        }
        public static void RemoveProducts()
        {
            var cacheKey = "PRODUCT_LIST";

            _cache.Remove(cacheKey);
        }
        public static void Clear()
        {
            foreach (var item in _cache)
            {
                _cache.Remove(item.Key);
            }

        }
    }
}