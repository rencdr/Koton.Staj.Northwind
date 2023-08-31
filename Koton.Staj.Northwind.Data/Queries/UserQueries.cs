

namespace Koton.Staj.Northwind.Data.Queries
{
    internal class UserQueries
    {
        internal const string CREATE_USE_REGISTER = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password); SELECT CAST(SCOPE_IDENTITY() as int)";
        internal const string AUTH_USER = "SELECT * FROM Users WHERE Username = @Username";
    }
}
