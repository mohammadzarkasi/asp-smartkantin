using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace smartkantin.Models;

public class Food
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public Guid VendorId { get; set; }
    public string Name { get; set; }
    public string? FoodPict { get; set; }
    public double Price { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }


    // navigation property
    [ForeignKey("VendorId")]
    public VendorAccount Vendor { get; set; }
}