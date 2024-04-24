using System.ComponentModel.DataAnnotations;

namespace smartkantin.Dto
{
    public class UpdateCartItemDto
    {
        [Required]
        public Guid Id { get; set; }


        [Required]
        [Range(1, 100)]
        public int Qty { get; set; }
    }
}