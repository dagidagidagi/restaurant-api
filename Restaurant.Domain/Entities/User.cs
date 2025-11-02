using Restaurant.Domain.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Entities
{
    public class User
    {
        [Column("user_id")]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public string Role { get; set; }
        public List<Delivery> Deliveries { get; set; } = new List<Delivery>(); 
        public UserDto AsDto()
        {
            return new UserDto
            {
                Id = this.Id,
                Name = this.Login,
                Email = this.Email,
                Role = this.Role,
            };
        }    
    }
}
