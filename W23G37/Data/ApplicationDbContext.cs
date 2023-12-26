using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using W23G37;
using W23G37.Models;

namespace W23G37.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Perdoruesi> Perdoruesit { get; set; }
        public DbSet<TeDhenatPerdoruesit> TeDhenatPerdoruesit { get; set; }
        public DbSet<Departamentet> Departamentet { get; set;}
        public DbSet<Lendet> Lendet { get; set; }
        public DbSet<LendetDepartamentiProfesori> LendetDepartamentiProfesori { get; set; }
        public DbSet<LlogaritERejaTeKrijuara> LlogaritERejaTeKrijuara { get; set; }
        public DbSet<Lokacionet> Lokacionet { get; set; }
        public DbSet<Sallat> Sallat { get; set; }
        public DbSet<LokacioniDepartamenti> LokacionetDepartamenti { get; set; }
        public DbSet<ParaqitjaSemestrit> ParaqitjaSemestrit { get; set; }
        public DbSet<NiveliStudimeve> NiveliStudimeve { get; set; }
        public DbSet<Semestri> Semestri { get; set; }
        public DbSet<AfatiParaqitjesSemestrit> AfatiParaqitjesSemestrit { get; set; }
        public DbSet<SpecializimetPerDepartament> SpecializimetPerDepartament { get; set; }
        public DbSet<TeDhenatRegjistrimitStudentit> TeDhenatRegjistrimitStudentit { get; set; }
        public DbSet<AplikimetEReja> AplikimetEReja { get; set; }
        public DbSet<NiveliStudimitDepartamenti> NiveliStudimitDepartamenti { get; set; }
        public DbSet<Bankat> Bankat {  get; set; }
        public DbSet<Pagesat> Pagesat { get; set; }
        public DbSet<Zbritjet> Zbritjet { get; set; }
        public DbSet<ZbritjaStudenti> ZbritjaStudenti { get; set; }
    }
}