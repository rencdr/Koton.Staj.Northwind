
using Koton.Staj.Northwind.Data.DataUtilities;
using Koton.Staj.Northwind.Entities.Concrete;

namespace Koton.Staj.Northwind.Data.Abstract
{
    public interface IUserRepository
    {
        Task<ResponseModel<User>> GetUserByUsernameAsync(string username);
        Task<ResponseModel<int>> CreateUserAsync(User user);

    }
}
