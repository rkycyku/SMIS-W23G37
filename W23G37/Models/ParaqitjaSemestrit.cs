using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace W23G37.Models
{
    public class ParaqitjaSemestrit
    {
        [Key]
        public int ParaqitjaSemestritID { get; set; }
        public int? APSID { get; set; }
        public int? SemestriID { get; set; }
        public int? StudentiID { get; set; }
        public int? LokacioniID { get; set; }
        public string? NderrimiOrarit { get; set; }
        public DateTime? DataParaqitjes { get; set; } = DateTime.Now;
        [ForeignKey(nameof(SemestriID))]
        public virtual Semestri? Semestri { get; set; }
        [ForeignKey(nameof(StudentiID))]
        [JsonIgnore]
        public virtual Perdoruesi? Studenti { get; set; }
        [ForeignKey(nameof(APSID))]
        public virtual AfatiParaqitjesSemestrit? AfatiParaqitjesSemestrit { get; set; }
        [ForeignKey(nameof(LokacioniID))]
        public virtual Lokacionet? Lokacioni { get; set; }
    }
}
