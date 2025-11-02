using Restaurant.Domain.Entities;
using Restaurant.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IBaseResponse<UserDto>> AuthUser(string login, string password);
        string GenerateToken(UserDto user);
    }
}
