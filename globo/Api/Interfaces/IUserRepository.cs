
using WebApplication17.Dtos;

public interface IUserRepository
    {
        UserEntity? GetByUsernameAndPassword(string username, string password);
    }