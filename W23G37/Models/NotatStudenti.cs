using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace W23G37.Models
{
    public class NotatStudenti
    {
        [Key]
        public int NotaStudentiID { get; set; }
        public int? LendaID { get; set; }
        public int? StudentiID { get; set; }
        public int? ParaqitjaProvimitID { get; set; }
        public int? Nota { get; set; }
        public DateTime? DataVendosjesSeNotes { get; set; } = DateTime.Now;
        [ForeignKey(nameof(LendaID))]
        public virtual Lendet? Lenda { get; set; }
        [ForeignKey(nameof(StudentiID))]
        [JsonIgnore]
        public virtual Perdoruesi? Studenti { get; set; }
        [ForeignKey(nameof(ParaqitjaProvimitID))]
        public virtual ParaqitjaProvimit? Provimi { get; set; }
    }
}
