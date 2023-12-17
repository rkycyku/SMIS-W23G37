﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace W23G37.Models
{
    public class LokacioniDepartamenti
    {
        [Key]
        public int LokacioniDepartamentiID { get; set; }

        public int? LokacioniID { get; set; }
        public int? DepartamentiID { get; set; }

        [ForeignKey("LokacioniID")]
        public virtual Lokacionet Lokacioni { get; set; }
        [ForeignKey("DepartamentiID")]
        public virtual Departamentet Departamentet { get; set;}

        public DateTime? DataKrijimit { get; set; } = DateTime.Now;
    }
}