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

        [NotMapped]
        public virtual MovieHall MovieHall { get; set; }

        [ForeignKey(nameof(MovieHall))]
        public int? MovieHallId { get; set; }
    }

}
