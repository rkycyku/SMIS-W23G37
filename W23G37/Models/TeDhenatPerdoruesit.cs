﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace W23G37.Models
{
    public class TeDhenatPerdoruesit
    {
        [Key]
        public int TeDhenatID { get; set; }
        public string? NrPersonal {  get; set; }
        public string? EmriPrindit { get; set; }
        public string? EmailPersonal { get; set; }
        public string? NrKontaktit { get; set; }

        public string? Qyteti { get; set; }

        public int? ZipKodi { get; set; }

        public string? Adresa { get; set; }

        public string? Shteti { get; set; }

        public string? Gjinia { get; set; }

        public DateTime? DataLindjes { get; set; }

        [ForeignKey("Perdoruesi")]
        public int UserID { get; set; }

        [JsonIgnore]
        public virtual Perdoruesi? User { get; set; }

        public DateTime? DataKrijimit { get; set; } = DateTime.Now;
    }
}
