using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using W23G37.Data;
using W23G37.Models;

namespace W23G37.Controllers
{
    [Authorize(Policy = "punonAdministrat")]
    public class AfatetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AfatetController(ApplicationDbContext context)
        {
            _context = context;
        }
        public class AfatetViewModel
        {
            public int? NiveliStudimeveZgjedhur { get; set; }
            public int? DepartamentiZgjedhur { get; set; }
            public AfatiParaqitjesSemestrit? APS { get; set; }
            public List<AfatiParaqitjesSemestrit>? APSList { get; set; }
            public NiveliStudimeve? NiveliStudimit { get; set; }
            public List<NiveliStudimeve>? NiveliStudimeve { get; set; }
            public ParaqitjaSemestrit? ParaqitjaSemestrit { get; set; }
            public List<ParaqitjaSemestrit>? ParaqitjaSemestrave { get; set; }
            public Semestri? Semestri { get; set; }
            public List<Semestri>? Semestrat { get; set; }
            public SpecializimetPerDepartament? SpecializimiPerDepartament { get; set; }
            public List<SpecializimetPerDepartament>? SpecializimetPerDepartament { get; set; }
            public Departamentet? Departamenti { get; set; }
            public List<Departamentet>? Departamentet { get; set; }
            public Dictionary<AfatiParaqitjesSemestrit, int>? APSListTotStudenteve { get; set; }
            public NiveliStudimitDepartamenti? NiveliStudimitDepartamenti { get; set; }
            public List<NiveliStudimitDepartamenti>? NiveliStudimitDepartamentiList { get; set; }
        }

        // GET: AfatetController
        public async Task<IActionResult> SemestriIndex()
        {
            var semestrat = await _context.Semestri.Include(x => x.NiveliStudimeve).ToListAsync();

            var modeli = new AfatetViewModel
            {
                Semestrat = semestrat
            };

            return View(modeli);
        }

        public async Task<IActionResult> NiveliStudimeveIndex()
        {
            var niveletStudimeve = await _context.NiveliStudimeve.ToListAsync();

            AfatetViewModel modeli = new()
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
        public async Task<IActionResult> ShtoniNivelinEStudimeve(AfatetViewModel a)
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

            AfatetViewModel modeli = new()
            {
                NiveliStudimit = niveliStudimit,
                Semestrat = semestrat,
                APSList = aps,
                NiveliStudimitDepartamentiList = NiveliStudimitDepartamenti
            };

            return View(modeli);
        }

        public async Task<IActionResult> ShtoniSemestrin()
        {
            var niveletEStudimit = await _context.NiveliStudimeve.ToListAsync();

            AfatetViewModel modeli = new()
            {
                NiveliStudimeve = niveletEStudimit,
            };

            return View(modeli);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShtoniSemestrin(AfatetViewModel a)
        {
            var niveletEStudimit = await _context.NiveliStudimeve.ToListAsync();
            var kontrolloSemestrin = await _context.Semestri.Include(x => x.NiveliStudimeve).Where(x => x.NiveliStudimeveID == a.NiveliStudimeveZgjedhur && x.NrSemestrit == a.Semestri.NrSemestrit).FirstOrDefaultAsync();

            AfatetViewModel modeli = new()
            {
                NiveliStudimeve = niveletEStudimit,
            };

            if (a.Semestri?.NrSemestrit < 0 || a.Semestri?.NrSemestrit > 12)
            {
                ModelState.AddModelError("Semestri.NrSemestrit", "Nr semestrit nuk duhet te jete i zbrazet dhe duhet te jete nga 1 deri ne 12!");
            }

            if(kontrolloSemestrin != null)
            {
                ModelState.AddModelError("Semestri.NrSemestrit", "Ky semester egziston per kete Nivel te Studimeve!");
            }

            if (ModelState.IsValid)
            {
                Models.Semestri semestri = new()
                {
                    NrSemestrit = a.Semestri?.NrSemestrit,
                    NiveliStudimeveID = a.NiveliStudimeveZgjedhur
                };

                await _context.Semestri.AddAsync(semestri);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Semestri u shtua me sukses!";

                return RedirectToAction("SemestriIndex");
            }

            return View(modeli);
        }

        public async Task<IActionResult> DetajetSemestri(int id)
        {
            var semestri = await _context.Semestri.Include(x => x.NiveliStudimeve).Where(x => x.SemestriID == id).FirstOrDefaultAsync();

            AfatetViewModel modeli = new()
            {
                Semestri = semestri,
                NiveliStudimit = semestri?.NiveliStudimeve
            };

            return View(modeli);
        }

        public async Task<IActionResult> SpecializimetPerDepartamentIndex()
        {
            var specializim = await _context.SpecializimetPerDepartament.Include(x => x.Departamenti).ToListAsync();

            AfatetViewModel modeli = new()
            {
                SpecializimetPerDepartament = specializim,
            };

            return View(modeli);
        }

        public async Task<IActionResult> SpecializimetPerDepartamentShtoni()
        {
            var departamentet = await _context.Departamentet.ToListAsync();

            AfatetViewModel modeli = new()
            {
                Departamentet = departamentet
            };

            return View(modeli);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SpecializimetPerDepartamentShtoni(AfatetViewModel a)
        {
            var departamentet = await _context.Departamentet.ToListAsync();

            AfatetViewModel modeli = new()
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

            AfatetViewModel modeli = new()
            {
                SpecializimiPerDepartament = specializimi,
            };

            return View(modeli);
        }
        public async Task<IActionResult> APSIndex()
        {
            var aps = await _context.AfatiParaqitjesSemestrit.Include(x => x.NiveliStudimeve).ToListAsync();

            var APSListaMeTotStudenteve = new Dictionary<AfatiParaqitjesSemestrit, int>();

            foreach(var afati in aps)
            {
                var paraqitjaSemestrit = await _context.PataqitjaSemestrit.Include(x => x.Semestri).Include(x => x.Studenti).Where(x => x.APSID == afati.APSID).ToListAsync();

                APSListaMeTotStudenteve.Add(afati, paraqitjaSemestrit.Count);
            }
            
            AfatetViewModel modeli = new()
            {
               APSListTotStudenteve = APSListaMeTotStudenteve
            };

            return View(modeli);
        }

        public async Task<IActionResult> APSDetaje(int id)
        {
            var aps = await _context.AfatiParaqitjesSemestrit.Include(x => x.NiveliStudimeve).Where(x => x.APSID == id).FirstOrDefaultAsync();

            var paraqitjaSemestrit = await _context.PataqitjaSemestrit.Include(x => x.Semestri).Include(x => x.Studenti).Where(x => x.APSID == id).ToListAsync();

            AfatetViewModel modeli = new()
            {
                APS = aps,
                ParaqitjaSemestrave = paraqitjaSemestrit,
            };

            return View(modeli);
        }
        public async Task<IActionResult> APSShtoni()
        {
            var NiveliStudimit = await _context.NiveliStudimeve.ToListAsync();

            var NiveletPaAPS = new List<NiveliStudimeve>();

            foreach(var niveli in NiveliStudimit)
            {
                var kontrolloAps = await _context.AfatiParaqitjesSemestrit.Where(x => x.NiveliStudimeveID == niveli.NiveliStudimeveID).OrderByDescending(x => x.APSID).FirstOrDefaultAsync();
                if (kontrolloAps != null)
                {
                    DateTime DataMbarimitAPS = (DateTime)kontrolloAps.DataMbarimitAfatit;
                    DateTime DataSotme = DateTime.Today;

                    if (DataMbarimitAPS < DataSotme)
                    {
                        NiveletPaAPS.Add(niveli);
                    }
                }
                else
                {
                    NiveletPaAPS.Add(niveli);
                }
            }

            AfatetViewModel modeli = new()
            {
                NiveliStudimeve = NiveletPaAPS,
            };

            return View(modeli);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> APSShtoni(AfatetViewModel a)
        {
            var NiveliStudimit = await _context.NiveliStudimeve.ToListAsync();

            AfatetViewModel modeli = new()
            {
                NiveliStudimeve = NiveliStudimit,
            };

            if (ModelState.IsValid)
            {
                string vitiAkademik;
                int vitiAktual = DateTime.Now.Year;

                if (a.APS.LlojiSemestrit == "dimëror")
                {
                    int vitiIArdhshem = vitiAktual + 1;
                    vitiAkademik = $"{vitiAktual}/{vitiIArdhshem}";
                }
                else
                {
                    int vitiIKaluar = vitiAktual - 1;
                    vitiAkademik = $"{vitiIKaluar}/{vitiAktual}";
                }

                AfatiParaqitjesSemestrit aps = new()
                {
                    LlojiSemestrit = a.APS.LlojiSemestrit,
                    NiveliStudimeveID = a.APS.NiveliStudimeveID,
                    DataFillimitAfatit = a.APS.DataFillimitAfatit,
                    DataMbarimitAfatit = a.APS.DataMbarimitAfatit,
                    VitiAkademik = vitiAkademik
                };

                await _context.AfatiParaqitjesSemestrit.AddAsync(aps);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Afati i Paraqitjes se Semestrit u shtua me sukses!";

                return RedirectToAction("APSIndex");
            }

            return View(modeli);
        }

        public async Task<IActionResult> VendosniNivelinEStudimeveNeDepartament()
        {
            var NiveliStudimit = await _context.NiveliStudimeve.ToListAsync();
            var Departamentet = await _context.Departamentet.ToListAsync();

            AfatetViewModel modeli = new()
            {
                NiveliStudimeve = NiveliStudimit,
                Departamentet = Departamentet,
            };

            return View(modeli);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VendosniNivelinEStudimeveNeDepartament(AfatetViewModel a)
        {
            var NiveliStudimit = await _context.NiveliStudimeve.ToListAsync();
            var Departamentet = await _context.Departamentet.ToListAsync();

            AfatetViewModel modeli = new()
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
    }
}
