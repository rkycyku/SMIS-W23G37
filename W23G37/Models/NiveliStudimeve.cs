using System.ComponentModel.DataAnnotations;

namespace W23G37.Models
{
    public class NiveliStudimeve
    {
        [Key]
        public int NiveliStudimeveID { get; set; }

        public string? EmriNivelitStudimeve { get; set; }
        public string? ShkurtesaEmritNivelitStudimeve { get; set; }
        public DateTime? DataKrijimit { get; set; } = DateTime.Now;

        public virtual List<Semestri>? Semestrat { get; set; }
    }
}
