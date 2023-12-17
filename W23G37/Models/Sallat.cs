using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace W23G37.Models
{
    public class Sallat
    {
        [Key] 
        public int SallaID { get; set; }
        public int KapacitetiSalles { get; set; }
        public int LokacioniID { get; set; }
        [ForeignKey("LokacioniID")]
        public virtual Lokacionet? Lokacioni { get; set; }
        public string? KodiSalles { get; set; }

        public DateTime? DataKrijimit { get; set; } = DateTime.Now;
    }
}
