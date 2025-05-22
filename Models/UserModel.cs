
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace srr.Models
{
    [Table("Users")]
    [Index("UserId", IsUnique = true)]
    public class UserModel
    {
        [DataType(DataType.Text), MaxLength(50)]
        public string? UserId { get; set; }
        public string? Name { get; set; }
        [DataType(DataType.EmailAddress), MaxLength(50)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
        public string? AddressNo { get; set; }

        [DataType(DataType.Text), MaxLength(8)]
        public string? DeedNo { get; set; } 
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}