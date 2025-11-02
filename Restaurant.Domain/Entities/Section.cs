using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Entities
{
    public class Section
    {
        public int Id {  get; set; }
        public string? Name { get; set; }
        public ICollection<Position> Positions { get; set; } = new List<Position>();    
    }
}
