using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Entities
{
    [Table("Delivery_Positions")]
    public class DeliveryPosition
    {
        [Column("delivery_id")]
        public int DeliveryId { get; set; }
        public Delivery? Delivery { get; set; }
        [Column("position_id")]
        public int PositionId { get; set; }
        public Position? Position { get; set; }
        public int Quantity { get; set;}
        public decimal Price { get; set;}

    }
}
