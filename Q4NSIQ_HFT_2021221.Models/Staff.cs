using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q4NSIQ_HFT_2021221.Models
{
    public class Staff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StaffID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Gender { get; set; }

        [Required]
        [StringLength(8, ErrorMessage = "IC must be 8 characters like 987654XY")]
        [RegularExpression("^[0-9]{6}[A-Za-z]{2}$", ErrorMessage = "IC must be like 987654XY")]
        public string IC { get; set; }

        [StringLength(11, ErrorMessage = "MobileNumber must be 11 characters")]
        [RegularExpression("^NULL$|^[0-9]{11}$", ErrorMessage = "MobileNumber must be like 36301234567")]
        public string MobileNumber { get; set; }

        [NotMapped]
        public virtual ICollection<Ticket> Tickets { get; set; }

        public Staff()
        {
            Tickets = new HashSet<Ticket>();
        }
    }
}
