using RedisAndMySqlDemo.Repository;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RedisAndMySqlDemo.Service
{
    

    public class ProductService
    {
        private readonly ProductRepository _repo;
        private readonly IDatabase _cache;

        public ProductService()
        {
            _repo = new ProductRepository();
            _cache = RedisManager.Db;
        }

 
        public List<PRDTB1> GetAll()
        {
            var cached = _cache.StringGet(CacheKeys.AllProducts);

            if (cached.HasValue)
            {
                return JsonConvert.DeserializeObject<List<PRDTB1>>(cached);
            }

            var products = _repo.GetAll();

            _cache.StringSet(
                CacheKeys.AllProducts,
                JsonConvert.SerializeObject(products),
                TimeSpan.FromMinutes(1));

            return products;
        }

        public PRDTB1 GetById(int id)
        {
            var key = CacheKeys.ProductById(id);
            var cached = _cache.StringGet(key);

            if (cached.HasValue)
            {
                return JsonConvert.DeserializeObject<PRDTB1>(cached);
            }

            var product = _repo.GetById(id);

            if (product != null)
            {
                _cache.StringSet(
                    key,
                    JsonConvert.SerializeObject(product),
                    TimeSpan.FromMinutes(10));
            }

            return product;
        }


        public void Update(PRDTB1 product)
        {
            _repo.Update(product);
            // remove from cache
            _cache.KeyDelete(CacheKeys.AllProducts);
            _cache.KeyDelete(CacheKeys.ProductById(product.PRDF01));
        }
    }

}