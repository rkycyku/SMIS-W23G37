using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace W23G37.Models
{
    public class LlogaritERejaTeKrijuara
    {
        [Key]
        public int LlogariaEReID { get; set; }

        public int? PerdoruesiID { get; set; }

        public string AspNetUserID { get; set; }

        [ForeignKey("AspNetUserID")]
        public IdentityUser AspNetUser { get; set; }

        [ForeignKey("PerdoruesiID")]
        public Perdoruesi? Perdoruesi { get; set; }

        public DateTime? DataKrijimit { get; set; } = DateTime.Now;

    }
}
