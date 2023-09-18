
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Koton.Staj.Northwind.Business.Abstract;
using Koton.Staj.Northwind.Business.Utilities;
using Koton.Staj.Northwind.Data.Abstract;
using Microsoft.Extensions.Configuration;
using BCrypt;
using Koton.Staj.Northwind.Business.Validation;
using Koton.Staj.Northwind.Entities.Concrete;
using FluentValidation;

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


        public async Task<ResponseModel<string>> AuthenticateUserAsync(User user)
        {


            var response = await _userRepository.GetUserByUsernameAsync(user.Username);

            if (response.Success)
            {
                var retrievedUser = response.Data;

                if (BCrypt.Net.BCrypt.Verify(user.Password, retrievedUser.Password))
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_jwtSecretKey);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                    new Claim(ClaimTypes.Name, retrievedUser.UserId.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddHours(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);

                    return new ResponseModel<string> { Success = true, Message = Messages.SUCCESS_MESSAGE, Data = tokenHandler.WriteToken(token) };
                }
            }

            return new ResponseModel<string> { Success = false, Message = Messages.INVALID_CREDENTIALS_MESSAGE, Data = null };
        }




        public async Task<ResponseModel<int>> RegisterUserAsync(User user)
        {

            var validator = new UserValidator();
            var validationResult = validator.Validate(user);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                return new ResponseModel<int> { Success = false, Message = string.Join(", ", errors), Data = -1 };
            }

            var existingUser = await _userRepository.GetUserByUsernameAsync(user.Username);


            if (existingUser == null)
            {
                return new ResponseModel<int> { Success = false, Message = Messages.USERNAME_ALREADY_EXISTS_MESSAGE, Data = -1 };
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            var newUser = new User
            {
                Username = user.Username,
                Password = hashedPassword
            };

            var createdUser = await CreateUserAsync(newUser);

            return createdUser != null
                ? new ResponseModel<int> { Success = true, Message = Messages.SUCCESS_MESSAGE, Data = 1 }
                : new ResponseModel<int> { Success = false, Message = Messages.USER_REGISTRATION_FAILED_MESSAGE, Data = -2 };


        }


        public async Task<ResponseModel<User>> CreateUserAsync(User user)
        {
            var response = new ResponseModel<User>();

            try
            {
                var userResponse = await _userRepository.CreateUserAsync(user);

                return userResponse.Success && userResponse.Data > 0
                ? new ResponseModel<User> { Success = true, Message = Messages.USER_CREATED_SUCCESS, Data = user }
                : new ResponseModel<User> { Success = false, Message = Messages.USER_CREATION_FAILED, Data = null };


            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Bir hata oluştu: " + ex.Message;
            }

            return response;
        }

        public async Task<ResponseModel<User>> GetUserByUsernameAsync(string username)
        {
            try
            {
                var userResponse = await _userRepository.GetUserByUsernameAsync(username);

                return userResponse.Success && userResponse.Data != null
                    ? new ResponseModel<User> { Success = true, Message = Messages.USER_FOUND, Data = userResponse.Data }
                    : new ResponseModel<User> { Success = false, Message = Messages.USER_NOT_FOUND };
            }
            catch (Exception ex)
            {
                return new ResponseModel<User> { Success = false, Message = "Bir hata oluştu: " + ex.Message };
            }
        }



    }
}







