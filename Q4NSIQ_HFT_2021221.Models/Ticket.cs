using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q4NSIQ_HFT_2021221.Models
{
    class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TicketID { get; set; }

        public string PaymentMethod { get; set; }

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
    }
}
