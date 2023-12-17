using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace W23G37.Models
{
    public class SpecializimetPerDepartament
    {
        [Key]
        public int SpecializimiID { get; set; }
        public string? EmriSpecializimit { get; set; }
        public int? DepartamentiID { get; set; }
        public DateTime? DataKrijimt { get; set; } = DateTime.Now;
        [ForeignKey(nameof(DepartamentiID))]
        public virtual Departamentet? Departamenti { get; set; }
    }
}
