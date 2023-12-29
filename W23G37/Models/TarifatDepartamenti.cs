using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace W23G37.Models
{
    public class TarifatDepartamenti
    {
        [Key]
        public int TarifaID { get; set; }
        public int? DepartamentiID { get; set; }
        public int? NiveliStudimitID { get; set; }
        public double? TarifaVjetore { get; set; }
        public DateTime? DataKrijimit { get; set; }= DateTime.Now;

        [JsonIgnore]
        [ForeignKey(nameof(DepartamentiID))]
        public virtual Departamentet? Departamenti { get; set; }
        [ForeignKey(nameof(NiveliStudimitID))]
        public virtual NiveliStudimeve? NiveliStudimeve { get; set; }
    }
}
