using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace smartkantin.Models;

public class VendorAccount
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    // [Column(TypeName = "VARCHAR")]
    // [StringLength(255)]
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string? PictPath { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }


    // navigation property
    [ForeignKey("UserId")]
    public MyUser User { get; set; }
}