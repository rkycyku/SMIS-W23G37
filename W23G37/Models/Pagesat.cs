using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace W23G37.Models
{
    public class Pagesat
    {
        [Key]
        public int PagesaID { get; set; }
        public int? BankaID { get; set; }
        public int? AplikimiID { get; set; }
        public int? PersoniID { get; set; }
        public string? Pagesa { get; set; }
        public string? Faturimi { get; set; }
        public string? PershkrimiPageses { get; set; }
        public string? LlojiPageses { get; set; }
        public DateTime? DataPageses {  get; set; }
        public DateTime? DataKrijimit { get; set; } = DateTime.Now;

        [ForeignKey(nameof(BankaID))]
        public virtual Bankat? Bankat { get; set; }
        [ForeignKey(nameof(PersoniID))]
        
        public virtual Perdoruesi? Perdoruesi { get; set; }
        [ForeignKey(nameof(AplikimiID))]
        public virtual AplikimetEReja? AplikimetEReja { get; set; }
    }
}
