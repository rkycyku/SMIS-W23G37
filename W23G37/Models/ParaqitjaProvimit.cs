using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace W23G37.Models
{
    public class ParaqitjaProvimit
    {
        [Key]
        public int ParaqitjaProvimitID { get; set; }
        public int? APPID { get; set; }
        public int? LDPID { get; set; }
        public int? StudentiID { get; set; } = null;
        public string? Nota { get; set; }
        public string? StatusiINotes { get; set; }
        public DateTime? DataVendosjesSeNotes { get; set; }
        public DateTime? DataParaqitjes { get; set; } = DateTime.Now;
        [ForeignKey(nameof(StudentiID))]
        [JsonIgnore]
        public virtual Perdoruesi? Studenti { get; set; } = null;
        [ForeignKey(nameof(APPID))]
        public virtual AfatiParaqitjesProvimit? AfatiParaqitjesProvimit { get; set; }
        [ForeignKey(nameof(LDPID))]
        public virtual LendetDepartamentiProfesori? LendetDepartamentiProfesori { get; set; }
    }
}
