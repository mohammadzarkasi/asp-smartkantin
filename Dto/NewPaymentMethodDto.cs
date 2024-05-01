using System.ComponentModel.DataAnnotations;

namespace smartkantin.Dto
{
    public class NewPaymentMethodDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Kode minimal terdiri dari 3 karakter")]
        [MaxLength(20, ErrorMessage = "Kode maksimal terdiri dari 20 karakter")]
        public string code { get; set; }

        [Required]
        [MaxLength(255)]
        public string name { get; set; }

        [Required]
        public int NeedConfirmation { get; set; }

        [Required]
        public int NeedUploadPayment { get; set; }
    }
}