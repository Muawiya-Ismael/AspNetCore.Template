using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Objects;

public class CustomerView : AView<Customer>
{
    public String Username { get; set; }
    public String? FirstName { get; set; }
    public String? LastName { get; set; }
    public String? Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public String Email { get; set; }
    public String? Phone { get; set; }
    public String? Country { get; set; }
    public String? Address { get; set; }
    public Boolean IsLocked { get; set; }
    public String? RoleTitle { get; set; }
}
