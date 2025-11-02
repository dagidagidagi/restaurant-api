using Restaurant.Domain.Entities;
using Restaurant.Domain.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.DTO
{
    public class CreateDeliveryDto
    {
        public List<PositionsDTO> Positions { get; set; } = new List<PositionsDTO>();
        public string Address {  get; set; }
    }
}
