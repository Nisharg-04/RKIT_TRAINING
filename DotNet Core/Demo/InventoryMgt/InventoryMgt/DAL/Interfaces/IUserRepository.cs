using InventoryMgt.MAL;

namespace InventoryMgt.DAL.Interfaces
{
    public interface IUserRepository
    {
        User GetByUsername(string username);
    }

}
