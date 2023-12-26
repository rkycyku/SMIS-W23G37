using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using W23G37.Data;
using W23G37.Models;

namespace W23G37.Areas.Studentet.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentetController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StudentetController(ApplicationDbContext context)
        {
            _context = context;
        }

        public class StudentetModel
        {
            public AfatiParaqitjesSemestrit? APS { get; set; }
            public List<Semestri>? Semestrat { get; set; }
            public List<Lokacionet>? Lokacionet { get; set; }
            public ParaqitjaSemestrit? ParaqitjaSemestrit { get; set; }
            public Perdoruesi? Perdoruesi { get; set; }
        }

        /*[Authorize]*/
        [AllowAnonymous]
        [HttpGet]
        [Route("ShfaqAfatinERegjistrimitSemestrit")]
        public async Task<IActionResult> ShfaqAfatinERegjistrimitSemestrit(string studentID)
        {
            var perdoruesi = await _context.Perdoruesit
                .Include(x => x.TeDhenatPerdoruesit)
                .Include(x => x.TeDhenatRegjistrimitStudentit)
                .ThenInclude(x => x.Departamentet)
                .ThenInclude(x => x.LokacionetDepartamenti)
                .ThenInclude(x => x.Lokacioni)
                .Include(x => x.TeDhenatRegjistrimitStudentit)
                .ThenInclude(x => x.NiveliStudimeve)
                .Where(x => x.AspNetUserId.Equals(studentID)).FirstOrDefaultAsync();

            var afatiParaqitjesSemestri = await _context.AfatiParaqitjesSemestrit.Where(x => x.NiveliStudimeveID == perdoruesi.TeDhenatRegjistrimitStudentit.NiveliStudimitID).ToListAsync();

            AfatiParaqitjesSemestrit aps = new();

            foreach (var afati in afatiParaqitjesSemestri)
            {
                if (afati.DataMbarimitAfatit > DateTime.Now)
                {
                    aps = afati;
                }
            }

            List<Semestri> semestrat = await _context.Semestri.Where(x => x.NiveliStudimeveID == perdoruesi.TeDhenatRegjistrimitStudentit.NiveliStudimitID).ToListAsync();

            List<Lokacionet> lokacioniet = new();

            var ld = await _context.LokacionetDepartamenti.Include(x => x.Lokacioni).Where(x => x.DepartamentiID == perdoruesi.TeDhenatRegjistrimitStudentit.DepartamentiID).ToListAsync();

            foreach (var l in ld)
            {
                lokacioniet.Add(l.Lokacioni);
            }

            StudentetModel modeli = new()
            {
                APS = aps,
                Semestrat = semestrat,
                Lokacionet = lokacioniet,
                Perdoruesi = perdoruesi
            };

            return Ok(modeli);
        }

        /*[Authorize]*/
        [AllowAnonymous]
        [HttpPost]
        [Route("RegjistroSemestrin")]
        public async Task<IActionResult> ShfaqAfatinERegjistrimitSemestrit(StudentetModel paraqitjaSemestrit)
        {
            
            await _context.ParaqitjaSemestrit.AddAsync(paraqitjaSemestrit.ParaqitjaSemestrit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", paraqitjaSemestrit.ParaqitjaSemestrit.ParaqitjaSemestritID, paraqitjaSemestrit.ParaqitjaSemestrit);
        }

        /*[Authorize]*/
        [AllowAnonymous]
        [HttpGet]
        [Route("ShfaqSemestratERegjistruar")]
        public async Task<IActionResult> ShfaqSemestratERegjistruar(string studentiID)
        {
            var perdoruesi = await _context.Perdoruesit.Where(x => x.AspNetUserId.Equals(studentiID)).FirstOrDefaultAsync();
            var paraqitjaSemestrit = await _context.ParaqitjaSemestrit
                .Include(x => x.AfatiParaqitjesSemestrit)
                .ThenInclude(x => x.NiveliStudimeve)
                .Include(x => x.Lokacioni)
                .Include(x => x.Semestri)
                .Where(x => x.StudentiID == perdoruesi.UserID)
                .ToListAsync();

            return Ok(paraqitjaSemestrit);
        }
    }
}
