using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Drawing.Drawing2D;
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
            public List<LendetDepartamentiProfesori>? LDPList { get; set; }
            public List<ParaqitjaProvimit>? ParaqitjaProvimitList { get; set; }
            public Dictionary<Lendet, List<Perdoruesi>>? LDPProfesoretList { get; set; }
            public List<Lendet>? Lendet { get; set; }
            public List<Provimi>? Provimet { get; set; }
            public AfatiParaqitjesProvimit? APP { get; set; }
            public Semestri? Semestri { get; set; }
            public List<NotatStudenti>? NotatStudentiList { get; set; }
            public List<Pagesat>? PagesatList { get; set; }
        }

        public class Provimi
        {
            public int? ParaqitjaProvimitID { get; set; }
            public string? EmriLendes { get; set; }
            public string? Nota { get; set; }
            public string? KategoriaLendes { get; set; }
            public string? Profesori { get; set; }
            public string? KodiLendes { get; set; }
            public string? StatusiNotes { get; set; }
            public DateTime? DataVendosjesNotes { get; set; }
            public DateTime? DataParaqitjes { get; set; }
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

            Semestri semestratPaPariqitur = new();

            foreach (var semestri in semestrat)
            {
                var kerko = await _context.ParaqitjaSemestrit.Include(x => x.Semestri).Where(x => x.SemestriID == semestri.SemestriID && x.StudentiID == perdoruesi.UserID).FirstOrDefaultAsync();

                if (kerko == null && semestratPaPariqitur.SemestriID == 0)
                {
                    semestratPaPariqitur = semestri;
                }

            }

            List<Lokacionet> lokacioniet = new();

            var ld = await _context.LokacionetDepartamenti.Include(x => x.Lokacioni).Where(x => x.DepartamentiID == perdoruesi.TeDhenatRegjistrimitStudentit.DepartamentiID).ToListAsync();

            var paraqitjaSemestrit = await _context.ParaqitjaSemestrit.Where(x => x.APSID == aps.APSID && x.StudentiID == perdoruesi.UserID).OrderByDescending(x => x.ParaqitjaSemestritID).FirstOrDefaultAsync();

            foreach (var l in ld)
            {
                lokacioniet.Add(l.Lokacioni);
            }

            StudentetModel modeli = new()
            {
                APS = aps,
                Semestri = semestratPaPariqitur,
                Lokacionet = lokacioniet,
                Perdoruesi = perdoruesi,
                ParaqitjaSemestrit = paraqitjaSemestrit
            };

            return Ok(modeli);
        }

        [Authorize]
        [HttpPost]
        [Route("RegjistroSemestrin")]
        public async Task<IActionResult> ShfaqAfatinERegjistrimitSemestrit(StudentetModel paraqitjaSemestrit)
        {

            await _context.ParaqitjaSemestrit.AddAsync(paraqitjaSemestrit.ParaqitjaSemestrit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", paraqitjaSemestrit.ParaqitjaSemestrit.ParaqitjaSemestritID, paraqitjaSemestrit.ParaqitjaSemestrit);
        }

        [Authorize]
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

        /*[Authorize]*/
        [AllowAnonymous]
        [HttpGet]
        [Route("ShfaqLendetPerParaqitjeProvimi")]
        public async Task<IActionResult> ShfaqLendetPerParaqitjeProvimi(string studentiID, int appID)
        {
            var perdoruesi = await _context.Perdoruesit.Include(x => x.TeDhenatRegjistrimitStudentit).Where(x => x.AspNetUserId.Equals(studentiID)).FirstOrDefaultAsync();
            var ldp = await _context.LendetDepartamentiProfesori.Include(x => x.Lendet).Include(x => x.Profesori).Where(x => x.DepartamentiID == perdoruesi.TeDhenatRegjistrimitStudentit.DepartamentiID && x.Pozita != "Asistent").ToListAsync();

            List<LendetDepartamentiProfesori>? lendetELejuaraPerParaqitje = new();

            foreach (var lenda in ldp)
            {
                var eshteIParaqitur = await _context.ParaqitjaProvimit.Where(x => x.LendetDepartamentiProfesori.LendaID == lenda.LendaID && x.StudentiID == perdoruesi.UserID && x.APPID == appID).FirstOrDefaultAsync();
                var eshteIParaqiturMeHeret = await _context.ParaqitjaProvimit.Where(x => x.LendetDepartamentiProfesori.LendaID == lenda.LendaID && x.StudentiID == perdoruesi.UserID && (!x.Nota.Equals("5") && !x.Nota.Equals("0"))).FirstOrDefaultAsync();
                var kaNotenEVendosur = await _context.NotatStudenti.Where(x => x.LendaID == lenda.LendaID && x.StudentiID == perdoruesi.UserID).FirstOrDefaultAsync();

                if (eshteIParaqitur == null && kaNotenEVendosur == null && eshteIParaqiturMeHeret == null)
                {
                    lendetELejuaraPerParaqitje.Add(lenda);
                }
            }

            var lendet = new List<Lendet>();

            foreach (var lendaNeLDP in lendetELejuaraPerParaqitje)
            {
                var lenda = lendaNeLDP.Lendet;

                if (!lendet.Contains(lenda))
                {
                    lendet.Add(lenda);
                }
            }

            List<ParaqitjaProvimit>? provimetEParaqitura = await _context.ParaqitjaProvimit.Where(x => x.APPID == appID && x.StudentiID == perdoruesi.UserID).ToListAsync();

            StudentetModel modeli = new()
            {
                Lendet = lendet,
                ParaqitjaProvimitList = provimetEParaqitura
            };

            return Ok(modeli);
        }

        /*[Authorize]*/
        [AllowAnonymous]
        [HttpGet]
        [Route("KontrolloAfatinParaqitjesProvimit")]
        public async Task<IActionResult> KontrolloAfatinParaqitjesProvimit(long data)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(data);
            DateTime dateTime = dateTimeOffset.DateTime;

            var app = await _context.AfatiParaqitjesProvimit.Where(x => x.DataFillimitAfatit <= dateTimeOffset && x.DataMbarimitAfatit >= dateTimeOffset).FirstOrDefaultAsync();

            return Ok(app);
        }

        /*[Authorize]*/
        [AllowAnonymous]
        [HttpPost]
        [Route("ParaqitniProvimin")]
        public async Task<IActionResult> ParaqitniProvimin(string studentiID, int ldPID, int appID)
        {
            var perdoruesi = await _context.Perdoruesit.Where(x => x.AspNetUserId.Equals(studentiID)).FirstOrDefaultAsync();

            if (perdoruesi == null)
            {
                return BadRequest();
            }

            ParaqitjaProvimit paraqitjaProvimit = new()
            {
                APPID = appID,
                LDPID = ldPID,
                StudentiID = perdoruesi.UserID
            };

            await _context.ParaqitjaProvimit.AddAsync(paraqitjaProvimit);
            await _context.SaveChangesAsync();

            return Ok();
        }

        /*[Authorize]*/
        [AllowAnonymous]
        [HttpGet]
        [Route("ShfaqProvimetEParaqitura")]
        public async Task<IActionResult> ShfaqProvimetEParaqitura(string studentiID)
        {
            var dataTani = DateTime.Now;
            var app = await _context.AfatiParaqitjesProvimit.Where(x => x.DataFunditShfaqjesProvimeve >= dataTani).FirstOrDefaultAsync();

            if (app != null)
            {
                var perdoruesi = await _context.Perdoruesit.Include(x => x.TeDhenatRegjistrimitStudentit).Where(x => x.AspNetUserId.Equals(studentiID)).FirstOrDefaultAsync();

                var provimetEParaqitura = await _context.ParaqitjaProvimit
                    .Include(x => x.LendetDepartamentiProfesori)
                    .ThenInclude(x => x.Lendet)
                    .Include(x => x.LendetDepartamentiProfesori)
                    .ThenInclude(x => x.Profesori)
                    .Where(x => x.StudentiID == perdoruesi.UserID && x.APPID == app.APPID).ToListAsync();

                List<Provimi> provimet = new();

                foreach (var provimi in provimetEParaqitura)
                {
                    Provimi prov = new()
                    {
                        ParaqitjaProvimitID = provimi.ParaqitjaProvimitID,
                        KodiLendes = provimi.LendetDepartamentiProfesori?.Lendet?.KodiLendes,
                        EmriLendes = provimi.LendetDepartamentiProfesori?.Lendet?.EmriLendes,
                        Profesori = (provimi.LendetDepartamentiProfesori?.Profesori?.Emri + " " + provimi.LendetDepartamentiProfesori?.Profesori?.Mbiemri).ToString(),
                        Nota = provimi.Nota,
                        DataParaqitjes = provimi.DataParaqitjes,
                        DataVendosjesNotes = provimi.DataVendosjesSeNotes,
                        StatusiNotes = provimi.StatusiINotes,
                        KategoriaLendes = provimi.LendetDepartamentiProfesori?.Lendet?.KategoriaLendes
                    };

                    provimet.Add(prov);
                }

                StudentetModel modeli = new()
                {
                    APP = app,
                    Provimet = provimet
                };

                return Ok(modeli);
            }

            return Ok(app);
        }

        /*[Authorize]*/
        [AllowAnonymous]
        [HttpDelete]
        [Route("AnuloParaqitjenProvimit")]
        public async Task<IActionResult> AnuloParaqitjenProvimit(int paraqitjaProvimitID)
        {
            var paraqitjaProvimit = await _context.ParaqitjaProvimit.Where(x => x.ParaqitjaProvimitID == paraqitjaProvimitID).FirstOrDefaultAsync();

            if (paraqitjaProvimit == null) { return BadRequest(); }

            _context.ParaqitjaProvimit.Remove(paraqitjaProvimit);
            await _context.SaveChangesAsync();

            return Ok();
        }

        /*[Authorize]*/
        [AllowAnonymous]
        [HttpDelete]
        [Route("QregjistroSemestrin")]
        public async Task<IActionResult> QregjistroSemestrin(string studentiID)
        {
            var perdoruesi = await _context.Perdoruesit.Include(x => x.TeDhenatRegjistrimitStudentit).Where(x => x.AspNetUserId.Equals(studentiID)).FirstOrDefaultAsync();
            var dataSot = DateTime.Now;

            var paraqitjaSemestrit = await _context.ParaqitjaSemestrit.Include(x => x.AfatiParaqitjesSemestrit)
                .Where(x => x.AfatiParaqitjesSemestrit.DataFillimitAfatit <= dataSot && x.AfatiParaqitjesSemestrit.DataMbarimitAfatit >= dataSot && x.StudentiID == perdoruesi.UserID)
                .FirstOrDefaultAsync();

            if (paraqitjaSemestrit == null) { return BadRequest(); }

            _context.ParaqitjaSemestrit.Remove(paraqitjaSemestrit);
            await _context.SaveChangesAsync();

            return Ok();
        }

        /*[Authorize]*/
        [AllowAnonymous]
        [HttpGet]
        [Route("ShfaqTranskriptenNotaveStudentit")]
        public async Task<IActionResult> ShfaqTranskriptenNotaveStudentit(string studentiID)
        {
            var perdoruesi = await _context.Perdoruesit
                .Include(x => x.TeDhenatRegjistrimitStudentit)
                .ThenInclude(x => x.Departamentet)
                .Include(x => x.TeDhenatRegjistrimitStudentit)
                .ThenInclude(x => x.NiveliStudimeve)
                .Include(x => x.TeDhenatPerdoruesit)
                .Where(x => x.AspNetUserId.Equals(studentiID))
                .FirstOrDefaultAsync();

            var notatStudentit = await _context.NotatStudenti.Include(x => x.Lenda).Include(x => x.Provimi).Where(x => x.StudentiID == perdoruesi.UserID).ToListAsync();

            StudentetModel modeli = new()
            {
                Perdoruesi = perdoruesi,
                NotatStudentiList = notatStudentit
            };

            return Ok(modeli);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ShfaqPagesatStudentit")]
        public async Task<IActionResult> ShfaqPagesatStudentit(string studentiID)
        {
            var perdoruesi = await _context.Perdoruesit
                .Include(x => x.TeDhenatRegjistrimitStudentit)
                .ThenInclude(x => x.Departamentet)
                .Include(x => x.TeDhenatRegjistrimitStudentit)
                .ThenInclude(x => x.NiveliStudimeve)
                .Include(x => x.TeDhenatPerdoruesit)
                .Where(x => x.AspNetUserId.Equals(studentiID))
                .FirstOrDefaultAsync();

            var pagesat = await _context.Pagesat.Include(x => x.Bankat).Where(x => x.PersoniID == perdoruesi.UserID).ToListAsync();

            List<Object> ListaPagesave = new List<Object>();


            double mbetja = 0;

            foreach (var pagesa in pagesat)
            {
                DateTime dataPageses = (DateTime)pagesa.DataPageses;
                var shkurtesBakes = pagesa.Bankat.KodiBankes;
                var urdheresa = $"{dataPageses.ToString("yy")}{dataPageses.Month:D2}{shkurtesBakes}";
                var viti = dataPageses.Month >= 9 ? $"{dataPageses.ToString("yy")}/{(dataPageses.AddYears(1)).ToString("yy")}" : $"{dataPageses.AddYears(-1).ToString("yy")}/{dataPageses.ToString("yy")}";


                if (pagesa.Pagesa != null)
                {
                    mbetja += Double.Parse(pagesa.Pagesa);
                }

                if (pagesa.KestiPageses != null)
                {
                    mbetja -= Double.Parse(pagesa.KestiPageses);
                }

                Object pagoi = new
                {
                    Lloji = pagesa.Pagesa != null ? "PAGESE" : "KESTIMUJOR",
                    Urdher = urdheresa,
                    Pershkrimi = pagesa.Pagesa != null ? $"{pagesa.Perdoruesi.Emri.ToUpper()} {pagesa.Perdoruesi.Mbiemri.ToUpper()}" : pagesa.PershkrimiPageses,
                    Viti = pagesa.KestiPageses != null ? $"{viti} {shkurtesBakes}" : viti,
                    Data = dataPageses,
                    Faturimi = pagesa.Pagesa != null ? 0 : Double.Parse(pagesa.KestiPageses),
                    Pagesa = pagesa.Pagesa != null ? Double.Parse(pagesa.Pagesa) : 0,
                    Mbetja = mbetja
                };

                ListaPagesave.Add(pagoi);
            }

            return Ok(ListaPagesave);
        }


        /*[Authorize]*/
        [AllowAnonymous]
        [HttpGet]
        [Route("ShfaqInfoPagesatStudentit")]
        public async Task<IActionResult> ShfaqInfoPagesatStudentit(string studentiID)
        {
            var perdoruesi = await _context.Perdoruesit
                .Include(x => x.TeDhenatRegjistrimitStudentit)
                .ThenInclude(x => x.Departamentet)
                .Include(x => x.TeDhenatRegjistrimitStudentit)
                .ThenInclude(x => x.NiveliStudimeve)
                .Include(x => x.TeDhenatPerdoruesit)
                .Where(x => x.AspNetUserId.Equals(studentiID))
                .FirstOrDefaultAsync();

            var pagesat = await _context.Pagesat.Include(x => x.Bankat).Where(x => x.PersoniID == perdoruesi.UserID).ToListAsync();

            Object model = new
            {
                Pagesat = pagesat.Where(x => x.Pagesa != null).Sum(x => double.Parse(x.Pagesa)),
                Faturimi = pagesat.Where(x => x.KestiPageses != null).Sum(x => double.Parse(x.KestiPageses)),
                Mbetja = pagesat.Where(x => x.Pagesa != null).Sum(x => double.Parse(x.Pagesa)) - pagesat.Where(x => x.KestiPageses != null).Sum(x => double.Parse(x.KestiPageses)),
                Niveli = $"{perdoruesi.TeDhenatRegjistrimitStudentit.NiveliStudimeve.EmriNivelitStudimeve}",
                Drejtimi = $"{perdoruesi.TeDhenatRegjistrimitStudentit.Departamentet.EmriDepartamentit}",
                AspNetUserID = perdoruesi.AspNetUserId,
                KodiFinanciar = perdoruesi.TeDhenatRegjistrimitStudentit.KodiFinanciar,
                Studenti = $"{perdoruesi.Emri.ToUpper()} {perdoruesi.Mbiemri.ToUpper()}",
                StudentiIDFK = perdoruesi.TeDhenatRegjistrimitStudentit.IdStudenti
            };

            return Ok(model);
        }

        /*[Authorize]*/
        [AllowAnonymous]
        [HttpPost]
        [Route("VendosTarifenMujore")]
        public async Task<IActionResult> VendosTarifenMujore(string studentiID)
        {
            var perdoruesi = await _context.Perdoruesit
                .Include(x => x.TeDhenatRegjistrimitStudentit)
                .ThenInclude(x => x.Departamentet)
                .Include(x => x.TeDhenatRegjistrimitStudentit)
                .ThenInclude(x => x.NiveliStudimeve)
                .Include(x => x.TeDhenatPerdoruesit)
                .Where(x => x.AspNetUserId.Equals(studentiID))
                .FirstOrDefaultAsync();

            var pagesatStudentit = await _context.Pagesat.Where(x => x.LlojiPageses.Equals("TarifaStudentit")).OrderByDescending(x => x.PagesaID).FirstOrDefaultAsync();

            DateTime dataFunditPagesaEFundit = new DateTime();

            int MuajiSot = DateTime.Today.Month;
            int VitiSot = DateTime.Today.Year;

            if (pagesatStudentit != null)
            {
                dataFunditPagesaEFundit = (DateTime)(pagesatStudentit.DataPageses);
            }
            else
            {
                dataFunditPagesaEFundit = new DateTime(VitiSot, MuajiSot + 1, 1);
            }

            var tarifaStudenti = await _context.TarifaStudenti.Include(x => x.TarifaStudimit).Include(x => x.Zbritja1).Include(x => x.Zbritja2).Where(x => x.StudentiID == perdoruesi.UserID).FirstOrDefaultAsync();

            var zbritja1TarifaFikse = tarifaStudenti?.Zbritja1 != null ? (tarifaStudenti.TarifaFikse / 12) * tarifaStudenti.Zbritja1.Zbritja / 100 : 0;
            var zbritja2TarifaFikse = tarifaStudenti?.Zbritja2 != null ? (tarifaStudenti.TarifaFikse / 12) * tarifaStudenti.Zbritja2.Zbritja / 100 : 0;

            var zbritja1TarifaStudimit = tarifaStudenti?.Zbritja1 != null ? (tarifaStudenti?.TarifaStudimit?.TarifaVjetore / 12) * (tarifaStudenti.Zbritja1.Zbritja / 100) : 0;
            var zbritja2TarifaStudimit = tarifaStudenti?.Zbritja2 != null ? (tarifaStudenti?.TarifaStudimit?.TarifaVjetore / 12) * (tarifaStudenti.Zbritja2.Zbritja / 100) : 0;

            var tarifaMujore = tarifaStudenti?.TarifaFikse != null ? (tarifaStudenti.TarifaFikse / 12 - zbritja1TarifaFikse - zbritja2TarifaFikse) : tarifaStudenti?.TarifaStudimit?.TarifaVjetore / 12 - zbritja1TarifaStudimit - zbritja2TarifaStudimit;

            var kaBerePagese = dataFunditPagesaEFundit.Month == MuajiSot ? true : false;

            var pagest = await _context.Pagesat.ToListAsync();

            if (!kaBerePagese)
            {
                Pagesat pagesa = new()
                {
                    BankaID = 0,
                    PersoniID = perdoruesi.UserID,
                    DataKrijimit = DateTime.Now,
                    DataPageses = new DateTime(VitiSot, MuajiSot, 01),
                    KestiPageses = tarifaMujore.ToString(),
                    LlojiPageses = "TarifaStudentit",
                    PershkrimiPageses = $"{VitiSot}{MuajiSot:D2} {pagest.Count + 1:D5}",
                };

                await _context.Pagesat.AddAsync(pagesa);
                await _context.SaveChangesAsync();

                return Ok(pagesa);
            }

            return Ok(dataFunditPagesaEFundit);
        }

    }
}
