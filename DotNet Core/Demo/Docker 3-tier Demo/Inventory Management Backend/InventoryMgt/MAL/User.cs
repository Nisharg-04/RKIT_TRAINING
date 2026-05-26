using ServiceStack.DataAnnotations;

namespace InventoryMgt.MAL
{
    [Alias("usrtb01")]
    public class User
    {
        [Alias("usrf01")]
        [AutoIncrement]
        public int Id { get; set; }

        [Alias("usrf02")]
        public string Username { get; set; }

        [Alias("usrf03")]
        public string Password { get; set; }

        [Alias("usrf04")]
        public string Role { get; set; }
    }

}
