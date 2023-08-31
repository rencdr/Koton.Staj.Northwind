
using System.Security.Claims;
using System.Text;
using BCrypt;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Koton.Staj.Northwind.Business.Abstract;
using Koton.Staj.Northwind.Business.Utilities;
using Koton.Staj.Northwind.Data.Abstract;
using Koton.Staj.Northwind.Entities;
using Microsoft.Extensions.Configuration;

namespace Koton.Staj.Northwind.Business.Concrete
{

    public class UserService : IUserService
    {


        private readonly IUserRepository _userRepository;
        private readonly string _jwtSecretKey;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _jwtSecretKey = configuration["JwtSecretKey"];
        }

        public async Task<User> CreateUserAsync(User user)
        {
            try
            {
                int userId = await _userRepository.CreateUserAsync(user);
                return userId > 0 ? user : null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }

        public ResponseModel AuthenticateUser(User user)
        {
            var retrievedUser = _userRepository.GetUserByUsernameAsync(user.Username).Result;

            if (retrievedUser != null && BCrypt.Net.BCrypt.Verify(user.Password, retrievedUser.Password))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSecretKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, retrievedUser.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new ResponseModel
                {
                    
                    Success = true,
                    Message = Messages.SUCCESS_MESSAGE,
                    Data = new { Token = tokenHandler.WriteToken(token) }
                };

            }
            else
            {
                return new ResponseModel
                {
                    
                    Success = false,
                    Message = Messages.INVALID_CREDENTIALS_MESSAGE,
                    Data = null
                };
            }
        }

         public ResponseModel RegisterUser(User user)

        {
            var existingUser = _userRepository.GetUserByUsernameAsync(user.Username).Result;

            if (existingUser != null)
            {
                return new ResponseModel
                {
                    
                    Success = false,
                    Message = Messages.USERNAME_ALREADY_EXISTS_MESSAGE,
                    Data = null
                };
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            var newUser = new User
            {
                Username = user.Username,
                Password = hashedPassword
            };

            var createdUser = CreateUserAsync(newUser).Result;

            return createdUser != null
                ? new ResponseModel {Success = true, Message = Messages.SUCCESS_MESSAGE, Data = createdUser }
                : new ResponseModel {Success = false, Message = Messages.USER_REGISTRATION_FAILED_MESSAGE, Data = null };
        }
    }
}


        



    
