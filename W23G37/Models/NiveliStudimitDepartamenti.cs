using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace W23G37.Models
{
    public class NiveliStudimitDepartamenti
    {
        [Key]
        public int NiveliStudimitDepartamentiID { get; set; }

        public int? NiveliStudimitID { get; set; }
        public int DepartamentiID { get; set; }
        [ForeignKey("NiveliStudimitID")]
        public virtual NiveliStudimeve NiveliStudimeve { get; set; }
        [ForeignKey("DepartamentiID")]
        public virtual Departamentet Departamentet { get; set;}

        public DateTime? DataKrijimit { get; set; } = DateTime.Now;
    }
}
