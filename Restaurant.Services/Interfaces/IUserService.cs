using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain.Entities;
using Restaurant.Domain.DTO;

namespace Restaurant.Services.Interfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<CreateUserDto>> RegistrUser(CreateUserDto user);
    }
}
