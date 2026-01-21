using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace RegenerateTokenDemo.Helpers
{
    public static class DbFactory
    {
        public static IDbConnectionFactory ConnectionFactory =
            new OrmLiteConnectionFactory(
                ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString,
                MySqlDialect.Provider);
    }
}