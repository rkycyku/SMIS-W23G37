using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using W23G37.Data;
using W23G37.Models;
using static W23G37.Controllers.TeNdryshemeController;

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
            public string? AfatiZgjedhur { get; set; }
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
            public AfatiParaqitjesProvimit? APP { get; set; }
            public List<AfatiParaqitjesProvimit>? APPList { get; set; }
            public Dictionary<AfatiParaqitjesProvimit, int>? APPListTotStudenteve { get; set; }

            public ParaqitjaProvimit? ParaqitjaProvimit { get; set; }
            public List<ParaqitjaProvimit>? ParaqitjaProvimeve { get; set; }

            public Dictionary<Perdoruesi, int>? ParaqitjaProvimitTotProvimeve { get; set; }
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

            if (kontrolloSemestrin != null)
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

        public async Task<IActionResult> APSIndex()
        {
            var aps = await _context.AfatiParaqitjesSemestrit.Include(x => x.NiveliStudimeve).ToListAsync();

            var APSListaMeTotStudenteve = new Dictionary<AfatiParaqitjesSemestrit, int>();

            foreach (var afati in aps)
            {
                var paraqitjaSemestrit = await _context.ParaqitjaSemestrit.Include(x => x.Semestri).Include(x => x.Studenti).Where(x => x.APSID == afati.APSID).ToListAsync();

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

            var paraqitjaSemestrit = await _context.ParaqitjaSemestrit
                .Include(x => x.Semestri)
                .Include(x => x.Studenti)
                .ThenInclude(x => x.TeDhenatRegjistrimitStudentit)
                .ThenInclude(x => x.Departamentet)
                .Include(x => x.Lokacioni)
                .Where(x => x.APSID == id).ToListAsync();

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

            foreach (var niveli in NiveliStudimit)
            {
                var kontrolloAps = await _context.AfatiParaqitjesSemestrit.Where(x => x.NiveliStudimeveID == niveli.NiveliStudimeveID).OrderByDescending(x => x.APSID).FirstOrDefaultAsync();
                if (kontrolloAps != null)
                {
                    DateTime DataMbarimitAPS = (DateTime)kontrolloAps.DataMbarimitAfatit;
                    DateTime DataSotme = new(2024,04,30);

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
        public async Task<IActionResult> APPIndex()
        {
            var app = await _context.AfatiParaqitjesProvimit.ToListAsync();

            var APPListaMeTotStudenteve = new Dictionary<AfatiParaqitjesProvimit, int>();

            foreach (var afati in app)
            {
                var paraqitjaProvimit = await _context.ParaqitjaProvimit.Where(x => x.APPID == afati.APPID).ToListAsync();

                APPListaMeTotStudenteve.Add(afati, paraqitjaProvimit.Select(pp => pp.StudentiID).Distinct().Count());
            }

            AfatetViewModel modeli = new()
            {
                APPListTotStudenteve = APPListaMeTotStudenteve
            };

            return View(modeli);
        }

        public async Task<IActionResult> APPDetaje(int id)
        {
            var app = await _context.AfatiParaqitjesProvimit
                .FirstOrDefaultAsync(x => x.APPID == id);

            if (app == null)
            {
                return NotFound();
            }

            var pp = await _context.ParaqitjaProvimit.Where(x => x.APPID == id)
                .ToListAsync();

            var IDStudenteve = pp.Select(p => p.StudentiID).Distinct().ToList();

            var Studentet = await _context.Perdoruesit
                .Include(s => s.TeDhenatRegjistrimitStudentit)
                    .ThenInclude(t => t.Departamentet)
                .Include(s => s.TeDhenatRegjistrimitStudentit)
                    .ThenInclude(t => t.NiveliStudimeve)
                .Where(s => IDStudenteve.Contains(s.UserID))
                .ToListAsync();

            var StudentiMeProvime = new Dictionary<Perdoruesi, int>();

            foreach (var IDStudenti in IDStudenteve)
            {
                var studenti = Studentet.FirstOrDefault(s => s.UserID == IDStudenti);

                if (studenti != null)
                {
                    var totProvimeveParaqitur = pp.Count(p => p.StudentiID == IDStudenti);
                    StudentiMeProvime.Add(studenti, totProvimeveParaqitur);
                }
            }

            AfatetViewModel modeli = new()
            {
                APP = app,
                ParaqitjaProvimeve = pp,
                ParaqitjaProvimitTotProvimeve = StudentiMeProvime
            };

            return View(modeli);
        }

        public ActionResult APPShtoni()
        {
            DateTime dataEDitesSotme = DateTime.Now;
            int vitiAktual = dataEDitesSotme.Year;
            int muajiAktual = dataEDitesSotme.Month;
            int vitiIKaluar = vitiAktual - 1;

            string vitiAkademik;

            if (muajiAktual >= 9)
            {
                vitiAkademik = vitiAktual + "/" + (vitiAktual + 1);
            }
            else
            {
                vitiAkademik = vitiIKaluar + "/" + vitiAktual;
            }

            AfatiParaqitjesProvimit app = new AfatiParaqitjesProvimit
            {
                VitiAkademik = vitiAkademik
            };

            AfatetViewModel modeli = new AfatetViewModel
            {
                APP = app
            };

            return View(modeli);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> APPShtoni(AfatetViewModel a)
        {
            DateTime dataEDitesSotme = DateTime.Now;
            int vitiAktual = dataEDitesSotme.Year;
            int muajiAktual = dataEDitesSotme.Month;
            int vitiIKaluar = vitiAktual - 1;

            string vitiAkademik;

            if (muajiAktual >= 9)
            {
                vitiAkademik = vitiAktual + "/" + (vitiAktual + 1);
            }
            else
            {
                vitiAkademik = vitiIKaluar + "/" + vitiAktual;
            }

            AfatiParaqitjesProvimit appv = new AfatiParaqitjesProvimit
            {
                VitiAkademik = vitiAkademik
            };

            var kontrolloAfatin = await _context.AfatiParaqitjesProvimit.Where(x => x.DataFillimitAfatit >= a.APP.DataFillimitAfatit && x.DataMbarimitAfatit <= a.APP.DataFillimitAfatit).FirstOrDefaultAsync();

            if (kontrolloAfatin != null)
            {
                ModelState.AddModelError("APP.DataFillimitAfatit", "Tashme keni shtuar nje afat qe eshte ne mes te ketyre datave!");
                ModelState.AddModelError("APP.DataMbarimitAfatit", "Tashme keni shtuar nje afat qe eshte ne mes te ketyre datave!");
            }

            if (ModelState.IsValid)
            {
                AfatiParaqitjesProvimit app = new()
                {
                    LlojiAfatit = a.APP.LlojiAfatit,
                    DataFillimitAfatit = a.APP.DataFillimitAfatit,
                    DataMbarimitAfatit = a.APP.DataMbarimitAfatit,
                    VitiAkademik = vitiAkademik,
                    Afati = a.AfatiZgjedhur,
                    DataFunditShfaqjesProvimeve = a.APP.DataFunditShfaqjesProvimeve
                };

                await _context.AfatiParaqitjesProvimit.AddAsync(app);
                await _context.SaveChangesAsync();

                TempData["Message"] = "Afati i Paraqitjes se Provimit u shtua me sukses!";

                return RedirectToAction("APPIndex");
            }

            AfatetViewModel modeli = new AfatetViewModel
            {
                APP = appv
            };

            return View(modeli);
        }

        public async Task<IActionResult> APPLargo(int id)
        {
            var app = await _context.AfatiParaqitjesProvimit.Where(x => x.APPID == id).FirstOrDefaultAsync();

            AfatetViewModel modeli = new()
            {
                APP = app,
            };

            return View(modeli);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> APPLargo(AfatetViewModel a)
        {
            var app = await _context.AfatiParaqitjesProvimit.Where(x => x.APPID == a.APP.APPID).FirstOrDefaultAsync();

            _context.AfatiParaqitjesProvimit.Remove(app);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Afati u fshi me sukses!";

            return RedirectToAction("APPIndex");
        }


    }
}
