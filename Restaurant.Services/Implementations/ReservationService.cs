using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.DAL.Interfaces;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Response;
using Restaurant.Domain.Enums;
using Restaurant.Domain.DTO;
using Restaurant.Services.Interfaces;

namespace Restaurant.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly IBaseRepository<ReservationsQuery> _reservationRepository;
        public ReservationService(IBaseRepository<ReservationsQuery> reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<IBaseResponse<ReservationsQuery>> AddQuery(CreateReservationsQuery data)
        {
            try
            {
                ReservationsQuery query = new ReservationsQuery()
                {
                    Name = data.Name,
                    PhoneNumber = data.PhoneNumber,
                    ReservationDate = DateOnly.Parse(data.ReservationDate),
                    ReservationTime = TimeOnly.Parse(data.ReservationTime),
                    Guests = data.Guests,
                    Format = data.Format,
                    ReservationComment = data.ReservationComment
                };
                await _reservationRepository.Create(query);
                return new BaseResponse<ReservationsQuery>
                {
                    StatusCode = StatusCode.OK,
                    Description = "Успешное создание запроса на бронирование",
                    Data = query
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ReservationsQuery>
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = ex.Message,
                };
            }
        }
    }
}
