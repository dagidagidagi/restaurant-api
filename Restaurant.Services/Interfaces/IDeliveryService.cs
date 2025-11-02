using Restaurant.Domain.DTO;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services.Interfaces
{
    public interface IDeliveryService
    {
        Task<IBaseResponse<Delivery>> CreateDelivery(CreateDeliveryDto data, UserDto user);
    }
}
