using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Domain.Entities
{
    public class Position
    {
        public int Id {  get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Column("section_id")]
        public int SectionId { get;set; }
        public Section? Section { get; set; }
        public decimal? Cost {  get; set; }
        [Column("image_source")]
        public string? ImageSource { get; set; }
        public List<Delivery> Deliveries { get; set; } = new List<Delivery>();
        public List<DeliveryPosition> DeliveryPositions { get; set; } = new List<DeliveryPosition>();
    }
}
