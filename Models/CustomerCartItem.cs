using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace smartkantin.Models
{
    public class CustomerCartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(255)]
        public string UserId { get; set; }
        
        [ForeignKey("TheFood")]
        public Guid FoodId { get; set; }
        public int Qty { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public Food TheFood {get;set;}
    }
}