using Restaurant.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Domain.Entities;
using Restaurant.Domain.DTO;
using Restaurant.Domain.Response;
using Restaurant.Domain.Enums;
using Restaurant.Services.Interfaces;
namespace Restaurant.Services.Implementations
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IBaseRepository<Delivery> _deliveriesRepository;
        public DeliveryService(IBaseRepository<Delivery> deliveryRepository)
        {
            _deliveriesRepository = deliveryRepository;
        }
        public async Task<IBaseResponse<Delivery>> CreateDelivery(CreateDeliveryDto data, UserDto user)
        {
            
            Delivery delivery = new Delivery()
            {
                UserId = user.Id,
                DeliveryDate = DateTime.Now,
                Status = "В обработке",
                Address = data.Address,
            };
            foreach (var position in data.Positions)
            {
                delivery.DeliveryPositions.Add(new DeliveryPosition() { PositionId = position.Id, Quantity = position.Quantity, Price = position.Cost * position.Quantity });
            }
            await _deliveriesRepository.Create(delivery);
            return new BaseResponse<Delivery>
            {
                StatusCode = StatusCode.OK,
                Description = "Успешное создание новой доставки",
                Data = delivery
            };
        }

    }
}
