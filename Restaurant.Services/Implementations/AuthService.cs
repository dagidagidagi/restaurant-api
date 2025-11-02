using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Entities;
using Restaurant.Domain.DTO;
using Restaurant.Domain.Options;
using Restaurant.Domain.Response;
using Restaurant.Domain.Enums;
using Restaurant.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Implementations
{
    public class AuthService : IAuthService, IUserService
    {

        private IBaseRepository<User> _userRepository;
        private IEncryptionService _encryptionService;
        public AuthService(IBaseRepository<User> userRepository, IEncryptionService encryptionService)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
        }

        public async Task<IBaseResponse<UserDto>> AuthUser(string login, string password)
        {
            try
            {
                string encryptedPassword = _encryptionService.Encrypt(password);
                var user = await _userRepository.GetAll()
                    .FirstOrDefaultAsync(user => user.Login == login && user.Password == encryptedPassword);
                if (user == null)
                {
                    return new BaseResponse<UserDto>
                    {
                        Description = "Неверный логин или пароль",
                        StatusCode = StatusCode.AccessDenied
                    };
                }
                UserDto userDto = user.AsDto();
                return new BaseResponse<UserDto>
                {
                    Description = $"Пользователь с логином: {login} аутентифицирован",
                    StatusCode = StatusCode.OK,
                    Data = userDto
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserDto>
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<CreateUserDto>> RegistrUser(CreateUserDto? createUserDto)
        {
            try
            {
                if (createUserDto == null)
                {
                    return new BaseResponse<CreateUserDto>
                    {
                        StatusCode = StatusCode.AccessDenied,
                        Description = "Неверный запрос"
                    };
                }
                if (string.IsNullOrWhiteSpace(createUserDto.Login) || string.IsNullOrWhiteSpace(createUserDto.Password))
                {
                    return new BaseResponse<CreateUserDto>
                    {
                        StatusCode = StatusCode.AccessDenied,
                        Description = "Логин и пароль не должны быть пустыми"
                    };
                }
                if (string.IsNullOrWhiteSpace(createUserDto.Email))
                {
                    return new BaseResponse<CreateUserDto>
                    {
                        StatusCode = StatusCode.AccessDenied,
                        Description = "Должна быть указана почта"
                    };
                }

                if (await _userRepository.GetAll().FirstOrDefaultAsync(user => user.Login == createUserDto.Login) != null)
                {
                    return new BaseResponse<CreateUserDto>
                    {
                        StatusCode = StatusCode.AccessDenied,
                        Description = $"Пользователь с логином {createUserDto.Login} уже существует"
                    };
                }
                ;
                User user = new User()
                {
                    Login = createUserDto.Login,
                    Password = _encryptionService.Encrypt(createUserDto.Password),
                    Email = createUserDto.Email,
                    Role = "user"
                };
                await _userRepository.Create(user);
                return new BaseResponse<CreateUserDto>
                {
                    StatusCode = StatusCode.OK,
                    Description = $"Успешная регистрация пользователя с логином {createUserDto.Login}"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CreateUserDto>
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = ex.Message
                };
            }
        }
        public string GenerateToken(UserDto user)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), new Claim(ClaimTypes.Name, user.Name), new Claim(ClaimTypes.Role, user.Role) };
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            string JWTtoken = new JwtSecurityTokenHandler().WriteToken(jwt);
            return JWTtoken;
        }
    }
}
