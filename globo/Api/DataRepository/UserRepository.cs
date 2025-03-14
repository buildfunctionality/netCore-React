using WebApplication17.Dtos;

namespace ConfArch.Data.Repositories
{
 
    public class UserRepository : IUserRepository
    {
        private List<UserEntity> users = new()
        {
            new UserEntity(3522, "admin", "admin", "blue", "Admin")
        };

        public UserEntity? GetByUsernameAndPassword(string username, string password)
        {
            var user = users.SingleOrDefault(u => u.Name == username && u.Password == password);
            return user;
        }
    }
}
