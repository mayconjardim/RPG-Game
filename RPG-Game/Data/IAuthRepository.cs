using RPG_Game.Models;

namespace RPG_Game.Data
{ 
    public interface IAuthRepository
    {

        Task<ServiceResponse<int>> Register(User user, string password);

        Task<ServiceResponse<string>> Login(User user, string password);

        Task<bool> UserExists(String username);

    }
}
