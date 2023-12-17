using System.ComponentModel.DataAnnotations;

namespace W23G37.Models
{
    public class Lokacionet
    {
        [Key]
        public int LokacioniID { get; set; }
        public string? EmriLokacionit {  get; set; }
        public string? ShkurtesaLokacionit {  get; set; }
        public string? AdresaLokacionit { get; set; }
        public string? QytetiLokacionit { get; set; }
        public DateTime? DataKrijimit { get; set; } = DateTime.Now;
    }
}
