using System.ComponentModel.DataAnnotations;

namespace smartkantin.Dto
{
    public class NewFoodDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Minimal 3 karakter")]
        public string Name { get; set; }

        [Required]
        [Range(1, 100000)]
        public double Price { get; set; }
    }
}