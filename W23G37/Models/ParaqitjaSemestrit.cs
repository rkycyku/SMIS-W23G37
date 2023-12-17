using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace W23G37.Models
{
    public class ParaqitjaSemestrit
    {
        [Key]
        public int ParaqitjaSemestritID { get; set; }
        public int? APSID { get; set; }
        public int? SemestriID { get; set; }
        public int? StudentiID { get; set; }
        public string? NderrimiOrarit { get; set; }
        public DateTime? DataParaqitjes { get; set; } = DateTime.Now;
        [ForeignKey(nameof(SemestriID))]
        public virtual Semestri? Semestri { get; set; }
        [ForeignKey(nameof(StudentiID))]
        public virtual Perdoruesi? Studenti { get; set; }
        [ForeignKey(nameof(APSID))]
        public virtual AfatiParaqitjesSemestrit? AfatiParaqitjesSemestrit { get; set; }
    }
}
