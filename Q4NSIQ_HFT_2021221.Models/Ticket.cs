using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q4NSIQ_HFT_2021221.Models
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TicketId { get; set; }

        public string PaymentMethod { get; set; }

        [NotMapped]
        public virtual Seats Seat { get; set; }

        [Required]
        [ForeignKey(nameof(Seat))]
        public int SeatId { get; set; }

        [Required]
        public int Price { get; set; }

        [NotMapped]
        public virtual Staff Staff { get; set; }

        [Required]
        [ForeignKey(nameof(Staff))]
        public int StaffId { get; set; }

        [NotMapped]
        public virtual Showtime Showtime { get; set; }

        [Required]
        [ForeignKey(nameof(Showtime))]
        public int ShowtimeId { get; set; }

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
            return TicketId * 59 +
                   SeatId * 5 +
                   StaffId * 2 +
                   ShowtimeId * 13 +
                   Price * 7 +
                   (PaymentMethod is null ? 0 : PaymentMethod.Length * 11);
        }
    }
}
