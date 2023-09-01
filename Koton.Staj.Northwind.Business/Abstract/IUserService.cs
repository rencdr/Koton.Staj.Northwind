
using Koton.Staj.Northwind.Business.Utilities;
using Koton.Staj.Northwind.Entities;

namespace Koton.Staj.Northwind.Business.Abstract
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(User user);
        Task<User> GetUserByUsernameAsync(string username);
        ResponseModel AuthenticateUser(User user);
        ResponseModel RegisterUser(User user);





    }
}
