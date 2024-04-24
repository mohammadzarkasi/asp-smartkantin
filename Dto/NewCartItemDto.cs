using System.ComponentModel.DataAnnotations;

namespace smartkantin.Dto
{
    public class NewCartItemDto
    {
        [Required]
        public Guid FoodId { get; set; }


        [Required]
        [Range(1, 100)]
        public int Qty { get; set; }
    }
}