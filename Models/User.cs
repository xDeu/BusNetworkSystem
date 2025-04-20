using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusNetworkSystem.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        [StringLength(50)]
        public string PhoneNum { get; set; }

        [Required]
        [StringLength(64)]
        public string PasswordHash { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }

        public double Balance { get; set; }

        [Required]
        public int Role { get; set; } // 0-Guest, 1-User, 2-Admin

        public virtual ICollection<Ticket> Tickets { get; set; }

        public User()
        {
            Name = string.Empty;
            Email = string.Empty;
            PhoneNum = string.Empty;
            PasswordHash = string.Empty;
            RegistrationDate = DateTime.Now;
            Role = 1;
            Tickets = new HashSet<Ticket>();
        }

        public enum UserRole
        {
            Guest = 0,
            User = 1,
            Admin = 2
        }
    }
}
