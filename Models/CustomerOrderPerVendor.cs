using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace smartkantin.Models
{
    public class CustomerOrderPerVendor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id {get;set;}
        public Guid OrderId {get;set;}
        public Guid VendorId {get;set;}
        public double TotalPrice { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        // navigation property
        [ForeignKey("VendorId")]
        public VendorAccount Vendor {get;set;}
        
        [ForeignKey("OrderId")]
        public CustomerOrder Order {get;set;}
      
        [InverseProperty("orderPerVendor")]
        public ICollection<CustomerOrderDetail> orderDetails {get;set;} = [];
    }
}