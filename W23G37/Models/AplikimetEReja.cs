using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace W23G37.Models
{
    public class AplikimetEReja
    {
        [Key]
        public int AplikimiID { get; set; }
        public string? Emri { get; set; }
        public string? Mbiemri { get; set; }
        public string? NrPersonal { get; set; }
        public string? EmriPrindit { get; set; }
        public string? EmailPersonal { get; set; }
        public string? NrKontaktit { get; set; }
        public string? Qyteti { get; set; }
        public int? ZipKodi { get; set; }
        public string? Adresa { get; set; }
        public string? Shteti { get; set; }
        public string? Gjinia { get; set; }
        public DateTime? DataLindjes { get; set; }
        public string? KodiFinanciar { get; set; }
        public int? DepartamentiID { get; set; }
        public int? NiveliStudimitID { get; set; }
        public string? VitiAkademikRegjistrim { get; set; }
        public string? LlojiRegjistrimit { get; set; }
        public int? LlojiKontrates { get; set; }
        public int? ZbritjaID { get; set; } = null;

        public DateTime? DataRegjistrimit { get; set; } = DateTime.Now;
        [ForeignKey(nameof(DepartamentiID))]
        public virtual Departamentet? Departamentet { get; set; }
        [ForeignKey(nameof(NiveliStudimitID))]
        public virtual NiveliStudimeve? NiveliStudimeve { get; set; }
        [ForeignKey(nameof(ZbritjaID))]
        public virtual Zbritjet? Zbritja { get; set; }

        [JsonIgnore]
        public virtual Pagesat? Pagesat { get; set; }
    }
}
