
using Koton.Staj.Northwind.Entities;

namespace Koton.Staj.Northwind.Data.Abstract
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<int> CreateUserAsync(User user);
    }
}
