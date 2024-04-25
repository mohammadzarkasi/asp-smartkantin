using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace smartkantin.Models
{
    public class CustomerOrderPerVendor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id {get;set;}

        [ForeignKey("Order")]
        public Guid OrderId {get;set;}

        [ForeignKey("Vendor")]
        public Guid VendorId {get;set;}
        public double TotalPrice { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public VendorAccount Vendor {get;set;}
        public CustomerOrder Order {get;set;}
    }
}