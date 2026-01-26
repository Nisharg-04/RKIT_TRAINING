using ProductsMinimalAPIWithVaidation.Models;

namespace ProductsMinimalAPIWithVaidation.Services
{

        public static class UserService
        {
            private static List<User> users = new()
        {
            new User { Id = 1, UserName = "admin", Email = "admin@test.com" }
        };

            public static List<User> GetAll() => users;

            public static User? GetById(int id) =>
                users.FirstOrDefault(u => u.Id == id);

            public static User Add(User user)
            {
                user.Id = users.Max(u => u.Id) + 1;
                users.Add(user);
                return user;
            }
        }
    

}
