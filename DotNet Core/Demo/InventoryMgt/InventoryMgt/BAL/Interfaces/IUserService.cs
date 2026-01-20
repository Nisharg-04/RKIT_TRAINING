using InventoryMgt.MAL;

namespace InventoryMgt.BAL.Interfaces
{
    public interface IUserService
    {
        public User Authenticate(string username, string password);
    }
}
