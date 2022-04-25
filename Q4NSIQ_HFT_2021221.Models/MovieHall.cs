using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Q4NSIQ_HFT_2021221.Models
{
    public class MovieHall
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovieHallId { get; set; }

        public string HallCategory { get; set; }

        [Required]
        public int NumberOfSeats { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Showtime> Showtimes { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Seats> Seats { get; set; }

        public MovieHall()
        {
            Showtimes = new HashSet<Showtime>();
            Seats = new HashSet<Seats>();
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
            return MovieHallId * 59 +
                   NumberOfSeats * 5 +
                   (HallCategory != null ? HallCategory.Length * 2 : 0);
        }
    }
}
