using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [PrimaryKey(nameof(UserID))]
    public class PatchUser : User
    {
        new public string? Email { get; set; }
        new public string? Username { get; set; }
        new public string? DisplayName { get; set; }
        new public string? Password { get; set; }
        new public int? PhoneNumber { get; set; }
    }
}
