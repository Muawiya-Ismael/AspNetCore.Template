using Microsoft.EntityFrameworkCore;

namespace AspNetCore.Objects;

[Index(nameof(Email), IsUnique = true)]
[Index(nameof(Username), IsUnique = true)]
public class Customer : AModel
{
    [StringLength(32, ErrorMessage = "Username cannot exceed 32 characters.")]
    public String Username { get; set; }

    [StringLength(32, ErrorMessage = "First name cannot exceed 32 characters.")]
    public String? FirstName { get; set; }

    [StringLength(32, ErrorMessage = "Last name cannot exceed 32 characters.")]
    public String? LastName { get; set; }

    [StringLength(64)]
    public String Passhash { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [StringLength(256, ErrorMessage = "Email cannot exceed 256 characters.")]
    public String Email { get; set; }

    [Phone(ErrorMessage = "Invalid phone number.")]
    [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
    public String? Phone { get; set; }

    [StringLength(256, ErrorMessage = "Address cannot exceed 256 characters.")]
    public String? Address { get; set; }

    public DateTime DateOfBirth { get; set; }

    [StringLength(10, ErrorMessage = "Gender cannot exceed 10 characters.")]
    public String? Gender { get; set; }

    [StringLength(64, ErrorMessage = "Country cannot exceed 64 characters.")]
    public String? Country { get; set; }

    public Boolean IsLocked { get; set; }

    public Int64? RoleId { get; set; }
    public virtual Role? Role { get; set; }
}
