
using Koton.Staj.Northwind.Business.Utilities;
using Koton.Staj.Northwind.Entities.Concrete;

namespace Koton.Staj.Northwind.Business.Abstract
{
    public interface IUserService
    {
        Task<ResponseModel<string>> AuthenticateUserAsync(User user);

        Task<ResponseModel<int>> RegisterUserAsync(User user);

        Task<ResponseModel<User>> CreateUserAsync(User user);

        Task<ResponseModel<User>> GetUserByUsernameAsync(string username);


    }
}
