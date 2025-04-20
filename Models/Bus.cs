using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusNetworkSystem.Models
{
    [Table("Buses")]
    public class Bus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BusID { get; set; }

        [Required]
        [StringLength(50)]
        public string LicencePlate { get; set; }

        [Required]
        [StringLength (50)]
        public string Model { get; set; }

        [Required]
        public int Capacity {  get; set; }

        [Required]
        public short YearOfManufacture {  get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }

        public Bus()
        {
            LicencePlate = string.Empty;
            Model = string.Empty;
            Schedules = new HashSet<Schedule>();
        }
    }
}
