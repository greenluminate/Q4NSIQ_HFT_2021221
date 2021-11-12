using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q4NSIQ_HFT_2021221.Models
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovieId { get; set; }

        [Required]
        public string MovieTitle { get; set; }

        public string Category { get; set; }

        public int? AgeRestriction { get; set; }

        [Required]
        public string Languages { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [RegularExpression("^NULL$|^[1-5]{1}$")]
        public int? Rating { get; set; }

        [NotMapped]
        public virtual ICollection<Showtime> Showtimes { get; set; }

        public Movie()
        {
            Showtimes = new HashSet<Showtime>();
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
            return MovieId * 59 +
                   MovieTitle.Length * 5 +
                   Languages.Length * 2 +
                   (int)Duration.TotalSeconds;
        }
    }
}
