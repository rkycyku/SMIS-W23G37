using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using W23G37.Data;
using W23G37.Models;

namespace W23G37.Controllers
{
    [Authorize(Policy = "punonAdministrat")]
    public class LendetController : Controller
    {
        private readonly SignInManager<IdentityUser> SignInManager;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public LendetController(SignInManager<IdentityUser> signInManager, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            SignInManager = signInManager;
            _context = context;
            _userManager = userManager;
        }

        public class LendetViewModel
        {
            public string? KategoriaLendesZgjedhur { get; set; }
            public int? SemestriLigjerimitZgjedhur { get; set; }
            public int? LendaZgjedhur { get; set; }
            public int? DepartamentiZgjedhur { get; set; }
            public int? ProfesoriZgjedhur { get; set; }
            public string? PozitaZgjedhur { get; set; }
            public Lendet? Lenda { get; set; }
            public List<Lendet>? Lendet { get; set; }
            public Departamentet? Departamenti { get; set; }
            public List<Departamentet>? Departamentet { get; set; }
            public Perdoruesi? Perdoruesi { get; set; }
            public List<Perdoruesi>? Perdoruesit { get; set; }
            public LendetDepartamentiProfesori? LDP { get; set; }
            public List<LendetDepartamentiProfesori>? LDPList { get; set; }
            public List<string>? RoletELejuaraPerzgjedhje { get; set; }
            public Dictionary<Departamentet, List<(Perdoruesi, string)>>? DepartamentetMeProfesore { get; set; }
        }

        // GET: LokacionetController
        public async Task<IActionResult> Index()
        {
            var lendet = await _context.Lendet.ToListAsync();

            var lendetList = new List<Lendet>();

            foreach (var lenda in lendet)
            {
                lendetList.Add(lenda);
            }

            var modeli = new LendetViewModel
            {
                Lendet = lendetList
            };

            return View(modeli);
        }

        // GET: LokacionetController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var lenda = await _context.Lendet.Where(x => x.LendaID == id).FirstOrDefaultAsync();

            var LDP = await _context.LendetDepartamentiProfesori
                .Include(x => x.Departamentet)
                .Include(x => x.Profesori)
                .Where(x => x.LendaID == id)
                .ToListAsync();

            var departamentet = new List<Departamentet>();

            var departamentetMeProfesor = new Dictionary<Departamentet, List<(Perdoruesi profesor, string pozita)>>();

            foreach (var lendaNeLDP in LDP)
            {
                var departamenti = lendaNeLDP.Departamentet;

                if (!departamentet.Contains(departamenti))
                {
                    departamentet.Add(departamenti);
                }
            }

            foreach (var lendaNeLDP in LDP)
            {
                var profesori = lendaNeLDP.Profesori;
                var departamenti = lendaNeLDP.Departamentet;
                var pozita = lendaNeLDP.Pozita;

                if (!departamentetMeProfesor.ContainsKey(departamenti))
                {
                    departamentetMeProfesor[departamenti] = new List<(Perdoruesi profesor, string pozita)>();
                }

                departamentetMeProfesor[departamenti].Add((profesori, pozita));
            }


            if (lenda == null)
            {
                return BadRequest("Lenda nuk egziston!");
            }

            var lendaJson = new LendetViewModel
            {
                Lenda = lenda,
                Departamentet = departamentet,
                DepartamentetMeProfesore = departamentetMeProfesor
            };

            return View(lendaJson);
        }

        public async Task<IActionResult> FshiProfoesorinPerLenden(int LendaID, int DepartamentiID, int ProfesoriID, string Pozita)
        {
            try
            {
                var LDP = await _context.LendetDepartamentiProfesori.Where(x => x.LendaID == LendaID
                && x.DepartamentiID == DepartamentiID
                && x.ProfesoriID == ProfesoriID
                && x.Pozita.Equals(Pozita)).FirstOrDefaultAsync();

                _context.LendetDepartamentiProfesori.Remove(LDP);
                await _context.SaveChangesAsync();

                TempData["Message"] = Pozita + "i/ja u fshi me sukses!";

                return RedirectToAction("Details", new { id = LendaID });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return View();
            }
        }


        // GET: LokacionetController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: LokacionetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LendetViewModel l)
        {
            if (string.IsNullOrWhiteSpace(l.Lenda?.EmriLendes) || !Regex.IsMatch(l.Lenda.EmriLendes, @"^[a-zA-Z][a-zA-Z0-9 /ëçÇË,-]*$"))
            {
                ModelState.AddModelError("Lenda.EmriLendes", "Emri Lendes nuk duhet te jete i zbrazet dhe duhet te permbaj vetem shkronja!");
            }
            if (string.IsNullOrWhiteSpace(l.Lenda?.ShkurtesaLendes) || !Regex.IsMatch(l.Lenda.ShkurtesaLendes, @"^[a-zA-Z][a-zA-Z0-9]{0,3}$"))
            {
                ModelState.AddModelError("Lenda.ShkurtesaLendes", "Shkurtesa Lendes nuk duhet te jete i zbrazet dhe duhet te permbaj me se shumti 4 shkronja!");
            }
            if (string.IsNullOrWhiteSpace(l.Lenda?.KreditELendes.ToString()) || (l.Lenda.KreditELendes < 0 && l.Lenda.KreditELendes > 20))
            {
                ModelState.AddModelError("Lenda.KreditELendes", "ECTS nuk duhet te jete e zbrazet dhe duhet te jete mes 1 ose 20!");
            }

            if (ModelState.IsValid)
            {

                var lendetCount = await _context.Lendet.CountAsync();

                var kodiLendes = $"{l.SemestriLigjerimitZgjedhur:D2}{l.Lenda?.ShkurtesaLendes?.ToString().ToUpper()}{lendetCount + 1:D3}";

                var lenda = new Models.Lendet
                {
                    KodiLendes = kodiLendes,
                    EmriLendes = l.Lenda?.EmriLendes,
                    ShkurtesaLendes = l.Lenda?.ShkurtesaLendes?.ToString().ToUpper(),
                    KategoriaLendes = l.KategoriaLendesZgjedhur,
                    KreditELendes = l.Lenda?.KreditELendes,
                    SemestriLigjerimit = l.SemestriLigjerimitZgjedhur,
                };

                await _context.Lendet.AddAsync(lenda);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        public async Task<IActionResult> ShtoLendenDepartament(int id)
        {
            var lenda = await _context.Lendet.Where(x => x.LendaID == id).FirstOrDefaultAsync();

            var departamentet = await _context.Departamentet.ToListAsync();

            var modeli = new LendetViewModel
            {
                Lenda = lenda,
                Departamentet = departamentet,
            };

            return View(modeli);
        }

        public async Task<IActionResult> ShtoProfesorinPerLende(int LendaID, int DepartamentiZgjedhur)
        {
            var lenda = await _context.Lendet.FirstOrDefaultAsync(x => x.LendaID == LendaID);
            var departamenti = await _context.Departamentet.FirstOrDefaultAsync(x => x.DepartamentiID == DepartamentiZgjedhur);

            var teGjithePerdoruesit = await _context.Perdoruesit.ToListAsync();
            var profesoretEShtuar = await _context.LendetDepartamentiProfesori
                .Where(x => x.LendaID == LendaID && x.DepartamentiID == DepartamentiZgjedhur)
                .Select(x => x.ProfesoriID)
                .ToListAsync();

            var profesoret = new List<Perdoruesi>();
            var userManager = _userManager;

            foreach (var perdoruesi in teGjithePerdoruesit)
            {
                var user = await _userManager.FindByIdAsync(perdoruesi.AspNetUserId);

                var eshteProfesor = await userManager.IsInRoleAsync(user, "Profesor");
                var eshteAsistent = await userManager.IsInRoleAsync(user, "Asistent");

                if (eshteProfesor || eshteAsistent)
                {
                    var rolet = 0;
                    if (eshteProfesor) rolet++;
                    if (eshteAsistent) rolet++;

                    var profesori = profesoretEShtuar.Count(x => x == perdoruesi.UserID);
                    if (profesori < rolet)
                    {
                        profesoret.Add(perdoruesi);
                    }
                }
            }

            var modeli = new LendetViewModel()
            {
                Lenda = lenda,
                Departamenti = departamenti,
                Perdoruesit = profesoret
            };

            return View(modeli);
        }

        public async Task<IActionResult> ShtoRolinPerProfesorin(int LendaID, int DepartamentiID, int ProfesoriZgjedhur)
        {
            var lenda = await _context.Lendet.Where(x => x.LendaID == LendaID).FirstOrDefaultAsync();
            var departamenti = await _context.Departamentet.Where(x => x.DepartamentiID == DepartamentiID).FirstOrDefaultAsync();
            var profesori = await _context.Perdoruesit.Where(x => x.UserID == ProfesoriZgjedhur).FirstOrDefaultAsync();

            var perdoruesi = await _userManager.FindByIdAsync(profesori.AspNetUserId);
            var roletPerdoruesit = await _userManager.GetRolesAsync(perdoruesi);

            var profesoriIShtuar = await _context.LendetDepartamentiProfesori
                .Where(x => x.LendaID == LendaID && x.DepartamentiID == DepartamentiID && x.ProfesoriID == ProfesoriZgjedhur)
                .FirstOrDefaultAsync();

            var roletPerPerzgjedhje = new List<string>();

            foreach (var roli in roletPerdoruesit)
            {
                if (roli.Equals("Profesor") || roli.Equals("Asistent"))
                {
                    if (profesoriIShtuar != null && !roli.Equals(profesoriIShtuar.Pozita))
                    {
                        roletPerPerzgjedhje.Add(roli);
                    }
                    else
                    {
                        roletPerPerzgjedhje.Add(roli);
                    }
                }
            }

            var modeli = new LendetViewModel()
            {
                Lenda = lenda,
                Departamenti = departamenti,
                Perdoruesi = profesori,
                RoletELejuaraPerzgjedhje = roletPerPerzgjedhje.ToList()
            };

            return View(modeli);
        }

        // POST: LokacionetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShtoRolinPerProfesorin(LendetViewModel l)
        {
            try
            {
                var lenda = new LendetDepartamentiProfesori
                {
                    DepartamentiID = l.Departamenti?.DepartamentiID,
                    ProfesoriID = l.Perdoruesi?.UserID,
                    LendaID = (int)(l.Lenda?.LendaID),
                    Pozita = l.PozitaZgjedhur
                };

                await _context.LendetDepartamentiProfesori.AddAsync(lenda);
                await _context.SaveChangesAsync();

                TempData["Message"] = l.PozitaZgjedhur + "i/ja u shtua me sukses!";

                return RedirectToAction("Details", new { id = l.Lenda.LendaID });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return View();
            }
        }
    }
}
