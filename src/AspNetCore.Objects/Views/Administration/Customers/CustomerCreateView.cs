using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Objects.Views.Administration.Customers
{
    public class CustomerCreateView : AView
    {
        [Required]
        [StringLength(32, ErrorMessage = "Username cannot exceed 32 characters.")]
        public string Username { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "First name cannot exceed 32 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "Last name cannot exceed 32 characters.")]
        public string LastName { get; set; }

        [StringLength(64)]
        public string Password { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(256, ErrorMessage = "Email cannot exceed 256 characters.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number.")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
        public string Phone { get; set; }

        [StringLength(256, ErrorMessage = "Address cannot exceed 256 characters.")]
        public string Address { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [StringLength(10, ErrorMessage = "Gender cannot exceed 10 characters.")]
        public string Gender { get; set; }

        [StringLength(64, ErrorMessage = "Country cannot exceed 64 characters.")]
        public string Country { get; set; }

        public Boolean IsLocked { get; set; }

        public Int64 RoleId { get; set; }
    }
}
