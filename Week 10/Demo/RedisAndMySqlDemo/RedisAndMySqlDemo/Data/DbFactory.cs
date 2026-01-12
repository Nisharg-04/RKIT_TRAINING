using ServiceStack.OrmLite;
using ServiceStack.OrmLite.MySql;

public static class DbFactory
{
    public static OrmLiteConnectionFactory Create()
    {
        return new OrmLiteConnectionFactory(
            "Server=localhost;Database=products_db;User Id=Admin;Password=gs@123;",
            MySqlDialect.Provider);
    }
}
