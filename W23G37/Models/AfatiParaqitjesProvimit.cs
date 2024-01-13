using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace W23G37.Models
{
    public class AfatiParaqitjesProvimit
    {
        [Key]
        public int APPID { get; set; }
        public string? Afati { get; set; }
        public string? LlojiAfatit { get; set; }
        public string? VitiAkademik { get; set; }
        public DateTime? DataFillimitAfatit {  get; set; }
        public DateTime? DataMbarimitAfatit { get; set; }
        public DateTime? DataFunditShfaqjesProvimeve { get; set; }
        public DateTime? DataKrijimit { get; set; } = DateTime.Now;
    }
}
