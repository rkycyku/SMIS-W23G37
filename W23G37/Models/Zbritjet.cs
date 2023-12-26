using System.ComponentModel.DataAnnotations;

namespace W23G37.Models
{
    public class Zbritjet
    {
        [Key]
        public int ZbritjaID { get; set; }
        public string? EmriZbritjes { get; set; }
        public double? Zbritja {  get; set; }
        public DateTime? DataKrijimit { get; set; } = DateTime.Now;
    }
}
