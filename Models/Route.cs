using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusNetworkSystem.Models
{
    [Table("Routes")]
    public class Route
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RouteID {  get; set; }

        [Required]
        [StringLength(255)]
        public string RouteName { get; set; }

        [Required]
        [StringLength(255)]
        public string StartLocation { get; set; }

        [Required]
        [StringLength(255)]
        public string EndLocation { get; set; }

        [Required]
        public decimal Distance { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }

        public Route()
        {
            RouteName = string.Empty;
            StartLocation = string.Empty;
            EndLocation = string.Empty;
            Schedules = new HashSet<Schedule>();
        }
    }
}
