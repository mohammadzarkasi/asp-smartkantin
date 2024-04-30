using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace smartkantin.Models
{
    [Table("app_user_x_role")]
    public class MyUserRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }


        // navigation property

        [ForeignKey("RoleId")]
        public MyRole Role { get; set; }


        [ForeignKey("UserId")]
        public MyUser User { get; set; }
    }
}