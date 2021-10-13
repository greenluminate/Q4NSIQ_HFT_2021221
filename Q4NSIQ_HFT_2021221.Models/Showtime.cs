using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q4NSIQ_HFT_2021221.Models
{
    public class Showtime
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShowtimeId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [NotMapped]
        public virtual Movie Movie { get; set; }

        [Required]
        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }

        [NotMapped]
        public virtual MovieHall MovieHall { get; set; }

        [Required]
        [ForeignKey(nameof(MovieHall))]
        public int MovieHallId { get; set; }

        [NotMapped]
        public virtual ICollection<Ticket> Tickets { get; set; }

        public Showtime()
        {
            Tickets = new HashSet<Ticket>();
        }
    }
}
