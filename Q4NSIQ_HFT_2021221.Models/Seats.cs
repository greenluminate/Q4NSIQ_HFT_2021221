using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q4NSIQ_HFT_2021221.Models
{
    public class Seats
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SeatId { get; set; }

        public string SeatCategory { get; set; }

        public int? SeatNumber { get; set; }

        [NotMapped]
        public virtual MovieHall MovieHall { get; set; }

        [ForeignKey(nameof(MovieHall))]
        public int? MovieHallId { get; set; }

        [NotMapped]
        public virtual ICollection<Ticket> Tickets { get; set; }

        public Seats()
        {
            Tickets = new HashSet<Ticket>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return GetHashCode() == obj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return SeatId * 59 +
                   (SeatNumber is null ? 0 : (int)SeatNumber * 5) +
                   (SeatCategory is null ? 0 : SeatCategory.Length * 2) +
                   (MovieHallId is null ? 0 : (int)MovieHallId * 73);
        }
    }
}
