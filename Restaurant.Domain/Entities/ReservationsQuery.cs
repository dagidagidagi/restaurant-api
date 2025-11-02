using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Entities
{
    [Table("Reservations_Queries")]
    public class ReservationsQuery
    {
        [Column("reservation_id")]
        public int Id { get; set; }
        public string Name {  get; set; }
        [Column("phone_number")]
        public string PhoneNumber {  get; set; }
        [Column("reservation_date")]
        public DateOnly ReservationDate {  get; set; }
        [Column("reservation_time")]
        public TimeOnly ReservationTime { get; set; }
        public int Guests {  get; set; }
        public string Format {  get; set; }
        [Column("reservation_comment")]
        public string ReservationComment {  get; set; }
    }
}
