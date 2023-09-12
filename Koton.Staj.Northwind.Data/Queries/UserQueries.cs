

namespace Koton.Staj.Northwind.Data.Queries
{
    internal class UserQueries
    {
        internal const string CREATE_USE_REGISTER = @"INSERT INTO users
            (username,
             password)
VALUES      (@Username,
             @Password);

SELECT Cast(Scope_identity() AS INT) ";
        internal const string AUTH_USER = @"SELECT *
FROM   users
WHERE  username = @Username ";
    }
}
