using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace W23G37.Models
{
    public class TeDhenatRegjistrimitStudentit
    {
        [Key]
        public int TDhRSID { get; set; }
        public string? KodiFinanciar { get; set; }
        public int? DepartamentiID { get; set; }
        public int? NiveliStudimitID { get; set; }
        public string? VitiAkademikRegjistrim { get; set; }
        public DateTime? DataRegjistrimit { get; set; } = DateTime.Now;
        public string? LlojiRegjistrimit { get; set; }
        public int? SpecializimiID { get; set; }
        public int UserId { get; set; }
        public string? IdStudenti { get; set; }

        [ForeignKey(nameof(DepartamentiID))]
        public virtual Departamentet? Departamentet { get; set;}
        [ForeignKey(nameof(NiveliStudimitID))]
        public virtual NiveliStudimeve? NiveliStudimeve { get; set; }
        [ForeignKey(nameof(SpecializimiID))]
        public virtual SpecializimetPerDepartament? Specializimi { get; set; }
        [ForeignKey(nameof(UserId))]

        [JsonIgnore]
        public virtual Perdoruesi? Perdoruesi { get; set; }
    }
}
