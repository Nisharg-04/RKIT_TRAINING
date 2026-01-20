using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ExpenseTracker.Common
{
    public static class OrmLiteConfig
    {
        public static OrmLiteConnectionFactory DbFactory;

        public static void Register()
        {
            DbFactory = new OrmLiteConnectionFactory(
                ConfigurationManager.ConnectionStrings["MySqlConn"].ConnectionString,
                MySqlDialect.Provider);
        }
    }

}