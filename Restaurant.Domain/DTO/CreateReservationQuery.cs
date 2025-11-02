using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.DTO
{
    public class CreateReservationsQuery
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string ReservationDate { get; set; }
        public string ReservationTime { get; set; }
        public int Guests { get; set; }
        public string Format { get; set; }
        public string ReservationComment { get; set; }
    }
}
