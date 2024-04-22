using System.ComponentModel.DataAnnotations;

namespace smartkantin.Dto
{
    public class NewVendorDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Minimal 3 karakter")]
        public string Name { get; set; }

    }
}