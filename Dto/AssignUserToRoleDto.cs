using System.ComponentModel.DataAnnotations;

namespace smartkantin.Dto
{
    public class AssignUserToRoleDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid RoleId { get; set; }
    }
}