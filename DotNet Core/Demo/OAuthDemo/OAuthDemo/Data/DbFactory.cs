using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace OAuthDemo.Data
{

    public class DbFactory
    {
        private readonly IConfiguration _config;

        public DbFactory(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnectionFactory Create()
        {
            return new OrmLiteConnectionFactory(
             _config.GetConnectionString("Default"),
         MySqlDialect.Provider
     );
        }
    }

}
