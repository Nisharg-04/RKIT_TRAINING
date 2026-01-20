using InventoryMgt.BAL.Interfaces;
using InventoryMgt.DAL.Interfaces;
using InventoryMgt.MAL;

namespace InventoryMgt.BAL
{
    public class UserHandler : IUserService
    {           
        private readonly IUserRepository _repo;

        public UserHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public User Authenticate(string username, string password)
        {
            var user = _repo.GetByUsername(username);
            if (user == null || user.Password != password)
                return null;

            return user;
        }
    }
}
