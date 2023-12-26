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

        public async Task<IActionResult> APSIndex()
        {
            var aps = await _context.AfatiParaqitjesSemestrit.Include(x => x.NiveliStudimeve).ToListAsync();

            var APSListaMeTotStudenteve = new Dictionary<AfatiParaqitjesSemestrit, int>();

            foreach(var afati in aps)
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

    }
}
