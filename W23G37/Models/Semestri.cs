using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace W23G37.Models
{
    public class Semestri
    {
        [Key]
        public int SemestriID { get; set; }
        public int? NrSemestrit {  get; set; }
        public int? NiveliStudimeveID { get; set; }
        public DateTime? DataKrijimit { get; set; } = DateTime.Now;
        [ForeignKey(nameof(NiveliStudimeveID))]
        public virtual NiveliStudimeve? NiveliStudimeve { get; set; }
    }
}
