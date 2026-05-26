using InventoryMgt.DAL.Interfaces;
using InventoryMgt.MAL;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace InventoryMgt.DAL
{
    public class UserContext : IUserRepository
    {
        private readonly IDbConnectionFactory _dbFactory;

        public UserContext(IDbConnectionFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public User GetByUsername(string username)
        {
            using var db = _dbFactory.Open();
            return db.Single<User>(u => u.Username == username);
        }
    }

}
