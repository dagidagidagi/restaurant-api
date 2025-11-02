using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Entities
{
    public class Delivery
    {
        [Column("delivery_id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        public User? User { get; set; }
        [Column("delivery_date")]
        public DateTime DeliveryDate { get; set; }
        public string Status { get; set; }
        public string Address {  get; set; }
        public List<Position> Positions { get; set; } = new List<Position>();
        public List<DeliveryPosition> DeliveryPositions { get; set; } = new List<DeliveryPosition>();
    }
}
