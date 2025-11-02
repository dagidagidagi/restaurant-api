using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain.Response;
using Restaurant.Domain.Entities;
using Restaurant.Domain.DTO;
namespace Restaurant.Services.Interfaces
{
    public interface IReservationService
    {
        Task<IBaseResponse<ReservationsQuery>> AddQuery(CreateReservationsQuery data);
    }
}
