using smartkantin.Models;

namespace smartkantin.Dto;
public class MyUserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }

    public static MyUserDto FromMyUser(MyUser user)
    {
        var result = new MyUserDto
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            Status = user.Status,
            CreatedOn = user.CreatedOn,
            UpdatedOn = user.UpdatedOn
        };

        return result;
    }
}