using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace smartkantin.Models
{
    public class CustomerOrderDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id {get;set;}
        // public Guid OrderId {get;set;}
        public Guid OrderPerVendorId {get;set;}
        public Guid FoodId {get;set;}
        public int Qty {get;set;}
        public double PriceSnapshot {get;set;}
        public string VendorNameSnapshot {get;set;}
        public string FoodNameSnapshot {get;set;}
        public double Subtotal {get;set;}

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }


        // navigation property
        [ForeignKey("OrderPerVendorId")]
        public CustomerOrderPerVendor orderPerVendor {get;set;}
    }
}