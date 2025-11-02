using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Interfaces
{
    public interface IPositionService
    {
        Task<IBaseResponse<IEnumerable<Position>>> GetAll();
        Task<IBaseResponse<IEnumerable<object>>> GetAllWithSections();
    }
}
