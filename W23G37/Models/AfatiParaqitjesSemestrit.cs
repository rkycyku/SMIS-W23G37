using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace W23G37.Models
{
    public class AfatiParaqitjesSemestrit
    {
        [Key]
        public int APSID { get; set; }
        public string? LlojiSemestrit { get; set; }
        public string? VitiAkademik { get; set; }
        public int? NiveliStudimeveID { get; set; }
        public DateTime? DataFillimitAfatit {  get; set; }
        public DateTime? DataMbarimitAfatit { get; set; }
        public DateTime? DataKrijimit { get; set; } = DateTime.Now;
        [ForeignKey(nameof(NiveliStudimeveID))]
        public virtual NiveliStudimeve? NiveliStudimeve { get; set; }
    }
}
