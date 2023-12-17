using System.ComponentModel.DataAnnotations;

namespace W23G37.Models
{
    public class Departamentet
    {
        [Key]
        public int DepartamentiID { get; set; }
        public string? EmriDepartamentit { get; set; }
        public string? ShkurtesaDepartamentit { get; set; }

        public DateTime? DataKrijimit { get; set; } = DateTime.Now;

        public virtual LokacioniDepartamenti? LokacionetDepartamenti { get; set; }
    }
}
