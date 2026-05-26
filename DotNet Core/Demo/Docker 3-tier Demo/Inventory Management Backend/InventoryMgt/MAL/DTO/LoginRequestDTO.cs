using System.ComponentModel;

namespace InventoryMgt.MAL.DTO
{
    public class LoginRequestDTO
    {
        [DefaultValue("manager")]
        public string Username { get; set; } 
        [DefaultValue("manager123")]
        public string Password { get; set; }
    }
}
