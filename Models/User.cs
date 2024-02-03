using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [PrimaryKey(nameof(UserID))]
    public class User
    {
        public Int64 UserID { get; set; }
        public byte[]? ProfilePic { get; set; }
        [NotMapped]
        public IFormFile? FormProfilePic { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public string? About { get; set; }
        public UserStatus? Userstatus { get; set; } = UserStatus.Offline;
        public string? CustomStatus { get; set; }
    }
}
