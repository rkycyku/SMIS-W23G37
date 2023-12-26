using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using W23G37.Data;
using W23G37.Models;
using static W23G37.Controllers.AfatetController;

namespace W23G37.Controllers
{
    [Authorize(Policy = "punonAdministrat")]
    public class TeNdryshemeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeNdryshemeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public class TeNdryshmeViewModel
        {
            public int? NiveliStudimeveZgjedhur { get; set; }
            public int? DepartamentiZgjedhur { get; set; }
            public List<AfatiParaqitjesSemestrit>? APSList { get; set; }
            public NiveliStudimeve? NiveliStudimit { get; set; }
            public List<NiveliStudimeve>? NiveliStudimeve { get; set; }
            public List<Semestri>? Semestrat { get; set; }
            public SpecializimetPerDepartament? SpecializimiPerDepartament { get; set; }
            public List<SpecializimetPerDepartament>? SpecializimetPerDepartament { get; set; }
            public List<Departamentet>? Departamentet { get; set; }
            public List<NiveliStudimitDepartamenti>? NiveliStudimitDepartamentiList { get; set; }
            public Zbritjet? Zbritja { get; set; }
            public List<Zbritjet>? Zbritjet { get; set; }
        }

        public async Task<IActionResult> NiveliStudimeveIndex()
        {
            var niveletStudimeve = await _context.NiveliStudimeve.ToListAsync();

            TeNdryshmeViewModel modeli = new()
            {
                NiveliStudimeve = niveletStudimeve
            };

            return View(modeli);
        }

        public ActionResult ShtoniNivelinEStudimeve()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShtoniNivelinEStudimeve(TeNdryshmeViewModel a)
        {
            if (string.IsNullOrWhiteSpace(a.NiveliStudimit?.EmriNivelitStudimeve) || !Regex.IsMatch(a.NiveliStudimit.EmriNivelitStudimeve, @"^[a-zA-z\s]+$"))
            {
                ModelState.AddModelError("NiveliStudimit.EmriNivelitStudimeve", "Emri nivelit te studimeve nuk duhet te jete i zbrazet dhe duhet te permbaje vetem shkronja!");
            }

            if (string.IsNullOrWhiteSpace(a.NiveliStudimit?.ShkurtesaEmritNivelitStudimeve) || !Regex.IsMatch(a.NiveliStudimit.ShkurtesaEmritNivelitStudimeve, @"^(?=.*[a-zA-Z])[a-zA-Z./]{1,8}$"))
            {
                ModelState.AddModelError("NiveliStudimit.ShkurtesaEmritNivelitStudimeve", "Shkurtesa emrit nivelit te studimeve nuk duhet te jete e zbrazet dhe duhet te permbaje vetem shkronja ose \". /\", me se shumti 8 karaktere!");
            }

            if (ModelState.IsValid)
            {
                await _context.NiveliStudimeve.AddAsync(a.NiveliStudimit);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Niveli studimeve u shtua me sukses!";

                return RedirectToAction("NiveliStudimeveIndex");
            }
            return View();
        }

        public async Task<IActionResult> DetajetNiveliStudimit(int id)
        {
            var niveliStudimit = await _context.NiveliStudimeve.Where(x => x.NiveliStudimeveID == id).FirstOrDefaultAsync();

            var semestrat = await _context.Semestri.Where(x => x.NiveliStudimeveID == id).ToListAsync();

            var aps = await _context.AfatiParaqitjesSemestrit.Include(x => x.NiveliStudimeve).Where(x => x.NiveliStudimeveID == id).ToListAsync();

            var NiveliStudimitDepartamenti = await _context.NiveliStudimitDepartamenti.Include(x => x.Departamentet).Where(x => x.NiveliStudimitID == id).ToListAsync();

            TeNdryshmeViewModel modeli = new()
            {
                NiveliStudimit = niveliStudimit,
                Semestrat = semestrat,
                APSList = aps,
                NiveliStudimitDepartamentiList = NiveliStudimitDepartamenti
            };

            return View(modeli);
        }


        public async Task<IActionResult> SpecializimetPerDepartamentIndex()
        {
            var specializim = await _context.SpecializimetPerDepartament.Include(x => x.Departamenti).ToListAsync();

            TeNdryshmeViewModel modeli = new()
            {
                SpecializimetPerDepartament = specializim,
            };

            return View(modeli);
        }

        public async Task<IActionResult> SpecializimetPerDepartamentShtoni()
        {
            var departamentet = await _context.Departamentet.ToListAsync();

            TeNdryshmeViewModel modeli = new()
            {
                Departamentet = departamentet
            };

            return View(modeli);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SpecializimetPerDepartamentShtoni(TeNdryshmeViewModel a)
        {
            var departamentet = await _context.Departamentet.ToListAsync();

            TeNdryshmeViewModel modeli = new()
            {
                Departamentet = departamentet
            };


            if (string.IsNullOrWhiteSpace(a.SpecializimiPerDepartament?.EmriSpecializimit) || !Regex.IsMatch(a.SpecializimiPerDepartament.EmriSpecializimit, @"^[a-zA-Z\s]+$"))
            {
                ModelState.AddModelError("SpecializimiPerDepartament.EmriSpecializimit", "Emri i specializmimit nuk duhet te jete i zbrazet dhe duhet te permbaje vetem shkronja!");
            }

            if (ModelState.IsValid)
            {
                SpecializimetPerDepartament specializimi = new()
                {
                    EmriSpecializimit = a.SpecializimiPerDepartament.EmriSpecializimit,
                    DepartamentiID = a.DepartamentiZgjedhur,
                };

                await _context.SpecializimetPerDepartament.AddAsync(specializimi);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Specializimi u shtua me sukses!";

                return RedirectToAction("SpecializimetPerDepartamentIndex");
            }

            return View(modeli);
        }

        public async Task<IActionResult> SpecializimetPerDepartamentDetaje(int id)
        {
            var specializimi = await _context.SpecializimetPerDepartament.Include(x => x.Departamenti).Where(x => x.SpecializimiID == id).FirstOrDefaultAsync();

            TeNdryshmeViewModel modeli = new()
            {
                SpecializimiPerDepartament = specializimi,
            };

            return View(modeli);
        }

        public async Task<IActionResult> VendosniNivelinEStudimeveNeDepartament()
        {
            var NiveliStudimit = await _context.NiveliStudimeve.ToListAsync();
            var Departamentet = await _context.Departamentet.ToListAsync();

            TeNdryshmeViewModel modeli = new()
            {
                NiveliStudimeve = NiveliStudimit,
                Departamentet = Departamentet,
            };

            return View(modeli);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VendosniNivelinEStudimeveNeDepartament(TeNdryshmeViewModel a)
        {
            var NiveliStudimit = await _context.NiveliStudimeve.ToListAsync();
            var Departamentet = await _context.Departamentet.ToListAsync();

            TeNdryshmeViewModel modeli = new()
            {
                NiveliStudimeve = NiveliStudimit,
                Departamentet = Departamentet,
            };

            if (ModelState.IsValid)
            {
                NiveliStudimitDepartamenti niveliStudimit = new()
                {
                    NiveliStudimitID = a.NiveliStudimeveZgjedhur,
                    DepartamentiID = (int)a.DepartamentiZgjedhur,
                };

                await _context.NiveliStudimitDepartamenti.AddAsync(niveliStudimit);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Niveli studimit u shtua me sukses per kete Departament!";

                return RedirectToAction("NiveliStudimeveIndex");
            }

            return View(modeli);
        }

        public async Task<IActionResult> ZbritjaIndex()
        {
            var zbritja = await _context.Zbritjet.ToListAsync();

            TeNdryshmeViewModel modeli = new()
            {
                Zbritjet = zbritja,
            };

            return View(modeli);
        }

        public ActionResult ZbritjaShtoni()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ZbritjaShtoni(TeNdryshmeViewModel a)
        {
            if (string.IsNullOrWhiteSpace(a.Zbritja?.EmriZbritjes) || !Regex.IsMatch(a.Zbritja?.EmriZbritjes, @"^[a-zA-ZçëÇË -]+$"))
            {
                ModelState.AddModelError("Zbritja.EmriZbritjes", "Lloji Zbritjes nuk duhet te jete i zbrazet dhe mund te permbaje vetem shkronja, hapesira dhe -!");
            }
            if (a.Zbritja?.Zbritja < 0.01)
            {
                ModelState.AddModelError("Zbritja.Zbritja", "Zbritja duhet te jete me e madhe se 0.01!");
            }

            if (ModelState.IsValid)
            {
                Zbritjet zbritja = new()
                {
                    EmriZbritjes = a.Zbritja.EmriZbritjes,
                    Zbritja = a.Zbritja.Zbritja,
                };

                await _context.Zbritjet.AddAsync(zbritja);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Zbritja u shtua me sukses!";

                return RedirectToAction("ZbritjaIndex");
            }

            return View();
        }
    }
}
