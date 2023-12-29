using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace W23G37.Models
{
    public class TarifaStudenti
    {
        [Key]
        public int ZbritjaStudentiID { get; set; }
        public int? StudentiID { get; set; }
        public int? TarifaStudimitID { get; set; } = null;
        public double? TarifaFikse { get; set; }
        public int? Zbritja1ID { get; set; } = null;
        public int? Zbritja2ID { get; set; } = null;
        public DateTime? DataKrijimit { get; set;} = DateTime.Now;

        [ForeignKey(nameof(StudentiID))]
        public virtual Perdoruesi? Studenti { get; set; }

        [ForeignKey(nameof(Zbritja1ID))]
        public virtual Zbritjet? Zbritja1 { get; set; }
        [ForeignKey(nameof(Zbritja2ID))]
        public virtual Zbritjet? Zbritja2 { get; set; }
        [ForeignKey(nameof(TarifaStudimitID))]
        public virtual TarifatDepartamenti? TarifaStudimit { get; set; }
    }
}
