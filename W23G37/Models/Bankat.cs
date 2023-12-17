using System.ComponentModel.DataAnnotations;

namespace W23G37.Models
{
    public class Bankat
    {
        [Key]
        public int BankaID { get; set; }
        public string? EmriBankes { get; set; }
        public string? KodiBankes { get; set; }
        public string? NumriLlogaris { get; set; }
        public string? AdresaBankes { get; set; }
        public string? BicKodi {  get; set; }
        public string? SwiftKodi { get; set; }
        public string? Valuta { get; set; }
        public string? IbanFillimi {  get; set; }
        public string? LlojiBankes { get; set; }
        public DateTime? DataKrijimit { get; set; } = DateTime.Now;
    }
}
