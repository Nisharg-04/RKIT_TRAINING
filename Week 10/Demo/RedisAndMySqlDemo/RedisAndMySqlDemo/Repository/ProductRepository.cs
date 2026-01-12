using ServiceStack.OrmLite;
using System.Collections.Generic;
using System.Linq;

namespace RedisAndMySqlDemo.Repository
{
    public class ProductRepository
    {
        private readonly OrmLiteConnectionFactory _dbFactory;

        public ProductRepository()
        {
            _dbFactory = DbFactory.Create();
        }

        public List<PRDTB1> GetAll()
        {
            var db = _dbFactory.Open();
            var res= db.Select<PRDTB1>();
            db.Close();
            return res;
          
        }

        public PRDTB1 GetById(int id)
        {
            var db = _dbFactory.Open();
            var res= db.SingleById<PRDTB1>(id);
            db.Close();
            return res;

        }

        public void Update(PRDTB1 product)
        {
            var db = _dbFactory.Open();
            db.Update(product);
            db.Close();
        }
    }
}