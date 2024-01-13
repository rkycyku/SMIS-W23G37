using System.ComponentModel.DataAnnotations;

namespace W23G37.Models
{
    public class Lendet
    {
        [Key]
        public int LendaID { get; set; }
        public string? KodiLendes { get; set; }
        public string? EmriLendes { get; set; }
        public string? ShkurtesaLendes { get; set; }
        public string? KategoriaLendes {  get; set; }
        public int? KreditELendes { get; set; }
        public int? SemestriLigjerimit { get; set; }

        public DateTime? DataKrijimit { get; set; } = DateTime.Now;

        public virtual List<LendetDepartamentiProfesori>? LDPList { get; set; }
    }
}
