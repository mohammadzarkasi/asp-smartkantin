using System.ComponentModel.DataAnnotations;

namespace smartkantin.Dto
{
    public class NewPaymentMethodDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string code {get;set;}

        [Required]
        [MaxLength(255)]
        public string name {get;set;}
    }
}