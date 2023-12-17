using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using W23G37.Data;
using W23G37.Models;

namespace W23G37.Controllers
{
    [Authorize(Policy = "punonAdministrat")]
    public class DepartamentetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartamentetController(ApplicationDbContext context)
        {
            _context = context;
        }

        public class DepartamentetViewModel
        {
            public Departamentet? Departamenti { get; set; }
            public List<Departamentet>? Departamentet { get; set; }
            public Lendet? Lenda { get; set; }
            public List<Lendet>? Lendet { get; set; }
            public Lokacionet? Lokacioni { get; set; }
            public List<Lokacionet>? Lokacionet { get; set; }
            public List<SpecializimetPerDepartament>? Specializimet { get; set; }
            public NiveliStudimitDepartamenti? NiveliStudimitDepartamenti { get; set; }
            public List<NiveliStudimitDepartamenti>? NiveliStudimitDepartamentiList { get; set; }
        }

        // GET: LokacionetController
        public async Task<IActionResult> Index()
        {
            var departamentet = await _context.Departamentet.ToListAsync();

            var departamentiJson = new DepartamentetViewModel
            {
                Departamentet = departamentet
            };

            return View(departamentiJson);
        }

        // GET: LokacionetController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var departamenti = await _context.Departamentet.Where(x => x.DepartamentiID == id).FirstOrDefaultAsync();

            var LDP = await _context.LendetDepartamentiProfesori
                .Include(x => x.Lendet)
                .Where(x => x.DepartamentiID == id)
                .ToListAsync();

            var lendet = new List<Lendet>();

            var lokacionet = new List<Lokacionet>();

            var LD = await _context.LokacionetDepartamenti.Include(x => x.Lokacioni).Where(x => x.DepartamentiID == id).ToListAsync();

            var SpD = await _context.SpecializimetPerDepartament.Where(x => x.DepartamentiID == id).ToListAsync();

            var NiveliStudimitDepartamenti = await _context.NiveliStudimitDepartamenti.Include(x => x.NiveliStudimeve).Where(x => x.DepartamentiID == id).ToListAsync();

            foreach (var lokacioniNeLD in LD)
            {
                var lokacioni = lokacioniNeLD.Lokacioni;

                if (!lokacionet.Contains(lokacioni))
                {
                    lokacionet.Add(lokacioni);
                }
            }

            foreach (var lendaNeLDP in LDP)
            {
                var lenda = lendaNeLDP.Lendet;

                if (!lendet.Contains(lenda))
                {
                    lendet.Add(lenda);
                }
            }

            if (departamenti == null)
            {
                return BadRequest("Departamenti nuk egziston!");
            }

            var departamentiJson = new DepartamentetViewModel
            {
                Departamenti = departamenti,
                Lendet = lendet,
                Lokacionet = lokacionet,
                Specializimet = SpD,
                NiveliStudimitDepartamentiList = NiveliStudimitDepartamenti
            };
            return View(departamentiJson);
        }

        // GET: LokacionetController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: LokacionetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartamentetViewModel d)
        {
            if (string.IsNullOrWhiteSpace(d.Departamenti?.EmriDepartamentit) || !Regex.IsMatch(d.Departamenti.EmriDepartamentit, @"^[a-zA-z\s]+$"))
            {
                ModelState.AddModelError("Departamenti.EmriDepartamentit", "Emri Departamentit nuk duhet te jete i zbrazet dhe duhet te permbaj vetem shkronja!");
            }
            if (string.IsNullOrWhiteSpace(d.Departamenti?.ShkurtesaDepartamentit) || !Regex.IsMatch(d.Departamenti.ShkurtesaDepartamentit, @"^[a-zA-Z]{1,4}$"))
            {
                ModelState.AddModelError("Departamenti.ShkurtesaDepartamentit", "Shkurtesa Departamentit nuk duhet te jete i zbrazet dhe duhet te permbaj me se shumti 4 shkronja!");
            }

            if (ModelState.IsValid)
            {
                await _context.Departamentet.AddAsync(d.Departamenti);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
