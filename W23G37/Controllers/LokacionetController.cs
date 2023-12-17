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
    public class LokacionetController : Controller
    {
        private readonly SignInManager<IdentityUser> SignInManager;
        private readonly ApplicationDbContext _context;

        public LokacionetController(SignInManager<IdentityUser> signInManager, ApplicationDbContext context)
        {
            SignInManager = signInManager;
            _context = context;
        }

        public class LokacionetViewModel
        {
            public int? DepartamentiZgjedhur { get; set; }
            public int? lokacioniZgjedhur { get; set; }
            public Lokacionet? Lokacioni { get; set; }
            public List<Lokacionet>? Lokacionet { get; set; }
            public Sallat? Salla { get; set; }
            public List<Sallat>? Sallat { get; set; }
            public Departamentet? Departamenti { get; set; }
            public List<Departamentet>? Departamentet { get; set; }
            public LokacioniDepartamenti? LD { get; set; }
            public List<LokacioniDepartamenti>? LDList { get; set; }
        }

        // GET: LokacionetController
        public async Task<IActionResult> Index()
        {
            var lokacionet = await _context.Lokacionet.ToListAsync();

            var lokacionetList = new List<LokacionetViewModel>();

            foreach (var lokacioni in lokacionet)
            {
                var lokacioniJson = new LokacionetViewModel
                {
                    Lokacioni = lokacioni,
                };

                lokacionetList.Add(lokacioniJson);
            }

            return View(lokacionetList);
        }

        // GET: LokacionetController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var lokacioni = await _context.Lokacionet.Where(x => x.LokacioniID == id).FirstOrDefaultAsync();
            var departamentetAktive = await _context.Departamentet.Include(x => x.LokacionetDepartamenti).Where(x => x.LokacionetDepartamenti.LokacioniID == id).ToListAsync();

            var sallat = new List<Sallat>(await _context.Sallat.Where(x => x.LokacioniID == id).ToListAsync());

            if (lokacioni == null)
            {
                return BadRequest("Lokacioni nuk egziston");
            }

            var lokacioniJson = new LokacionetViewModel
            {
                Lokacioni = lokacioni,
                Departamentet = departamentetAktive,
                Sallat = sallat,
            };

            return View(lokacioniJson);
        }

        // GET: LokacionetController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: LokacionetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LokacionetViewModel l)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(l.Lokacioni?.AdresaLokacionit))
                {
                    ModelState.AddModelError("Lokacioni.AdresaLokacionit", "Adresa Lokacionit nuk duhet te jete e zbrazet!");
                }

                if (string.IsNullOrWhiteSpace(l.Lokacioni?.EmriLokacionit) || !Regex.IsMatch(l.Lokacioni.EmriLokacionit, @"^[a-zA-Z\s]+$"))
                {
                    ModelState.AddModelError("Lokacioni.EmriLokacionit", "Emri Lokacionit nuk duhet te jete i zbrazet dhe duhet te permbaje vetem shkronja!");
                }

                if (string.IsNullOrWhiteSpace(l.Lokacioni?.QytetiLokacionit) || !Regex.IsMatch(l.Lokacioni.QytetiLokacionit, @"^[a-zA-Z\s]+$"))
                {
                    ModelState.AddModelError("Lokacioni.QytetiLokacionit", "Qyteti Lokacionit nuk duhet te jete i zbrazet dhe duhet te permbaje vetem shkronja!");
                }

                if (string.IsNullOrWhiteSpace(l.Lokacioni?.ShkurtesaLokacionit) || !Regex.IsMatch(l.Lokacioni.ShkurtesaLokacionit, @"^[a-zA-Z]$"))
                {
                    ModelState.AddModelError("Lokacioni.ShkurtesaLokacionit", "Shkurtesa Lokacionit nuk mund te jete e zbrazet dhe duhet te jete vetem nje shkronje!");
                }

                if (ModelState.IsValid)
                {
                    try
                    {

                        await _context.Lokacionet.AddAsync(l.Lokacioni);
                        await _context.SaveChangesAsync();

                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        return View();
                    }
                }
            }
            return View();
        }

        // POST: LokacionetController/Delete/5
        public async Task<IActionResult> Fshij(int LokacioniID)
        {
            try
            {
                var lokacioni = await _context.Lokacionet.Where(x => x.LokacioniID == LokacioniID).FirstOrDefaultAsync();

                if (lokacioni == null)
                {
                    return BadRequest("Lokacioni nuk egziston");
                }

                _context.Lokacionet.Remove(lokacioni);
                await _context.SaveChangesAsync();



                TempData["Message"] = "Lokacioni u fshi me Sukses!";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LokacionetController
        public async Task<IActionResult> IndexSallat()
        {
            var sallat = await _context.Sallat.Include(x => x.Lokacioni).ToListAsync();

            var sallatList = new List<LokacionetViewModel>();

            foreach (var salla in sallat)
            {
                var sallaJson = new LokacionetViewModel
                {
                    Salla = salla,
                    Lokacioni = salla.Lokacioni
                };

                sallatList.Add(sallaJson);
            }

            return View(sallatList);
        }

        // GET: LokacionetController/Details/5
        public async Task<IActionResult> DetailsSallat(int id)
        {
            var salla = await _context.Sallat.Include(x => x.Lokacioni).Where(x => x.SallaID == id).FirstOrDefaultAsync();

            if (salla == null)
            {
                return BadRequest("Salla nuk egziston");
            }

            var sallaJson = new LokacionetViewModel
            {
                Salla = salla, 
                Lokacioni = salla.Lokacioni
            };

            return View(sallaJson);
        }

        // GET: LokacionetController/Delete/5
        public async Task<IActionResult> DeleteSallat(int id)
        {
            var salla = await _context.Sallat.Include(x => x.Lokacioni).Where(x => x.SallaID == id).FirstOrDefaultAsync();

            if (salla == null)
            {
                return BadRequest("Salla nuk egziston");
            }

            var sallaJson = new LokacionetViewModel
            {
                Salla = salla,
                Lokacioni = salla.Lokacioni
            };

            return View(sallaJson);
        }

        // POST: LokacionetController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FshijSallat(int SallaID)
        {
            try
            {
                var salla = await _context.Sallat.Include(x => x.Lokacioni).Where(x => x.SallaID == SallaID).FirstOrDefaultAsync();

                if (salla == null)
                {
                    return BadRequest("Salla nuk egziston");
                }

                _context.Sallat.Remove(salla);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LokacionetController/Create
        public async Task<IActionResult> ShtoniSallat()
        {
            var lokacionetList = new List<Lokacionet>(await _context.Lokacionet.ToListAsync());

            var model = new LokacionetViewModel()
            {
                Lokacionet = lokacionetList
            };

            return View(model);
        }

        // POST: LokacionetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShtoniSallat(LokacionetViewModel s)
        {
            var lokacionetList = new List<Lokacionet>(await _context.Lokacionet.ToListAsync());

            var model = new LokacionetViewModel()
            {
                Lokacionet = lokacionetList
            };

            if (string.IsNullOrWhiteSpace(s.Salla?.KapacitetiSalles.ToString()) || s.Salla.KapacitetiSalles < 0)
            {
                ModelState.AddModelError("Salla.KapacitetiSalles", "Numri salles duhet te jete me i madhe se 0!");
            }

            if (ModelState.IsValid)
            {
                var lokacioni = await _context.Lokacionet.Where(x => x.LokacioniID == s.lokacioniZgjedhur).FirstOrDefaultAsync();
                var sallatCount = await _context.Sallat.Where(x => x.LokacioniID == s.lokacioniZgjedhur).CountAsync();

                if (lokacioni == null)
                {
                    return BadRequest("Lokacioni ose Salla nuk egzistojne");
                }

                var kodiSalles = $"{lokacioni?.ShkurtesaLokacionit.ToString().ToUpper()}{lokacioni?.LokacioniID}{sallatCount + 1:D3}";

                var salla = new Models.Sallat
                {
                    KapacitetiSalles = s.Salla.KapacitetiSalles,
                    LokacioniID = (int)s.lokacioniZgjedhur,
                    KodiSalles = kodiSalles
                };

                await _context.Sallat.AddAsync(salla);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> ShtoDepartamentinLokacion(int id)
        {
            var lokacioni = await _context.Lokacionet
                                        .Where(x => x.LokacioniID == id)
                                        .FirstOrDefaultAsync();

            var departamentetAktive = await _context.LokacionetDepartamenti
                                                  .Where(x => x.LokacioniID == id)
                                                  .Select(x => x.DepartamentiID)
                                                  .ToListAsync();

            var departamentetELejuaraSelektim = await _context.Departamentet
                                                    .Where(d => !departamentetAktive.Contains(d.DepartamentiID))
                                                    .ToListAsync();

            var modeli = new LokacionetViewModel()
            {
                Lokacioni = lokacioni,
                Departamentet = departamentetELejuaraSelektim,
            };

            return View(modeli);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShtoDepartamentinLokacion(LokacionetViewModel l)
        {
            try
            {
                var lokacioniDepartamenti = new Models.LokacioniDepartamenti
                {
                    LokacioniID = l.Lokacioni.LokacioniID,
                    DepartamentiID = l.DepartamentiZgjedhur,
                };

                await _context.LokacionetDepartamenti.AddAsync(lokacioniDepartamenti);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Departamenti u shtua me sukses!";

                return RedirectToAction("Details", new { id = l.Lokacioni.LokacioniID });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return View();
            }

        }

        public async Task<IActionResult> FshiDepartamentinLokacion(int LokacioniID, int DepartamentiID)
        {
            try
            {
                var departamenti = await _context.LokacionetDepartamenti.Include(x => x.Departamentet).Where(x => x.LokacioniID == LokacioniID && x.DepartamentiID == DepartamentiID).FirstOrDefaultAsync();

                _context.LokacionetDepartamenti.Remove(departamenti);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Departamenti i " + departamenti.Departamentet.ShkurtesaDepartamentit + " - s u fshi me sukses!";

                return RedirectToAction("Details", new { id = LokacioniID });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return View();
            }
        }
    }
}
