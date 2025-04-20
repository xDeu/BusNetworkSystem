using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusNetworkSystem.Models
{
    [Table("Schedules")]
    public class Schedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScheduleID { get; set; }

        [Required]
        [ForeignKey("Route")]
        public int RouteID { get; set; }

        [Required]
        [ForeignKey("Bus")]
        public int BusID { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        [Required]
        public int AvailibleSeats {  get; set; }

        public virtual Route Route { get; set; }
        public virtual Bus Bus { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }

        public Schedule()
        {
            Route = new Route();
            Bus = new Bus();
            Tickets = new HashSet<Ticket>();
        }
    }
}
