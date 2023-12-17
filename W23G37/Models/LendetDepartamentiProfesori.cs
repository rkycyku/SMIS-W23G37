using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace W23G37.Models
{
    public class LendetDepartamentiProfesori
    {
        [Key]
        public int LDPID { get; set; }
        public int LendaID { get; set; }
        public int? DepartamentiID { get; set; }
        public int? ProfesoriID { get; set; }
        public string? Pozita { get; set; }

        [ForeignKey("LendaID")]
        public virtual Lendet? Lendet { get; set; }
        [ForeignKey("DepartamentiID")]
        public virtual Departamentet? Departamentet { get; set; }
        [ForeignKey("ProfesoriID")]
        public virtual Perdoruesi? Profesori { get; set; }

        public DateTime? DataKrijimit { get; set; } = DateTime.Now;

    }
}
