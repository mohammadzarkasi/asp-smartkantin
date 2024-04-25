using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace smartkantin.Models
{
    public class CustomerOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        [Column(TypeName = "VARCHAR")]
        [StringLength(255)]
        public string CustomerId { get; set; }
        // public Guid VendorId { get; set; }
        public Guid PaymentMethodId { get; set; }
        public double TotalPrice { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? PaymentExpiredAt { get; set; }
        public DateTime? PaymentDoneAt { get; set; }
    }
}