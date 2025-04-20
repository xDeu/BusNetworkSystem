using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusNetworkSystem.Models
{
    [Table("Tickets")]
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TicketID { get; set; }

        [Required]
        [ForeignKey("Schedule")]
        public int ScheduleID {  get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserID {  get; set; }

        [Required]
        public int SeatNumber { get; set; }

        [Required]
        public decimal Price {  get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        public virtual Schedule Schedule { get; set; }
        public virtual User User { get; set; }

        public Ticket()
        {
            Schedule = new Schedule();
            User = new User();
        }
    }
}
