using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using W23G37.Data;
using W23G37.Models;
using static W23G37.Areas.Financat.Controllers.FinancatController;

namespace W23G37.Areas.Administrata.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrataController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> UserManager;
        public AdministrataController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            UserManager = userManager;
        }

        public class AdministrataModel
        {
            public virtual AplikimetEReja? AplikimetEReja { get; set; }
            public virtual TarifatDepartamenti? TarifaDepartamenti { get; set; }
            public virtual Perdoruesi? Perdoruesi { get; set; }
            public virtual TeDhenatPerdoruesit? TeDhenatPerdoruesit { get; set; }
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("shfaqDepartamentet")]
        public async Task<IActionResult> ShfaqDepartmanetet()
        {
            var departamentet = await _context.Departamentet.Include(x => x.TarifatDepartamenti).Where(x => x.TarifatDepartamenti.Count > 0).ToListAsync();

            return Ok(departamentet);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("shfaqNiveletStudimitGjatAplikimit")]
        public async Task<IActionResult> ShfaqNiveletStudimitGjatAplikimit(int departamentiID)
        {
            var niveletEStudimit = await _context.NiveliStudimitDepartamenti.Include(x => x.NiveliStudimeve).Include(x => x.Departamentet).Where(x => x.DepartamentiID == departamentiID).ToListAsync();

            return Ok(niveletEStudimit);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("gjeneroKodinFinanciar")]
        public async Task<IActionResult> GjeneroKodinFinanciar(int departamentiID, int niveliStudimitID)
        {
            var totaliStudentaveDepartament = await _context.TeDhenatRegjistrimitStudentit.Where(x => x.DepartamentiID == departamentiID).CountAsync();
            var totStudentaveAplikimIRiDepartament = await _context.AplikimetEReja.Where(x => x.DepartamentiID == departamentiID).CountAsync();

            var NrRadhesStudentit = totaliStudentaveDepartament + totStudentaveAplikimIRiDepartament + 1;

            string kodiFinanciar = $"{departamentiID:D2}{niveliStudimitID:D2}{NrRadhesStudentit:D5}";

            return Ok(kodiFinanciar);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GjeneroVitinAkademik")]
        public async Task<IActionResult> GjeneroVitinAkademik()
        {
            DateTime dataEDitesSotme = new DateTime(2023, 9, 11);
            int vitiAktual = dataEDitesSotme.Year;

            DateTime fillimiRegjistrimeve = new DateTime(vitiAktual, 6, 1);
            DateTime mbarimiRegjistrimeve = new DateTime(vitiAktual, 10, 7);

            string vitiAkademik;

            if (dataEDitesSotme >= fillimiRegjistrimeve && dataEDitesSotme <= mbarimiRegjistrimeve)
            {
                vitiAkademik = vitiAktual + "/" + (vitiAktual + 1);
            }
            else
            {
                vitiAkademik = "Nuk ka afat te hapur";
            }

            return Ok(vitiAkademik);

        }

        [Authorize(Policy = "eshteStaf")]
        [HttpPost]
        [Route("KrijoniAplikimERi")]
        public async Task<IActionResult> KrijoniAplikimERi([FromBody] AdministrataModel aplikimi)
        {
            await _context.AplikimetEReja.AddAsync(aplikimi.AplikimetEReja);
            await _context.SaveChangesAsync();

            return Ok(aplikimi);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("ShfaqZbritjet")]
        public async Task<IActionResult> ShfaqZbritjet()
        {
            var zbritjet = await _context.Zbritjet.Where(x => x.LlojiZbritjes != "Extra").ToListAsync();

            return Ok(zbritjet);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("ShfaqAplikimetEReja")]
        public async Task<IActionResult> ShfaqAplikimetEReja()
        {
            var aplikimetEReja = await _context.AplikimetEReja.Include(x => x.Departamentet).Include(x => x.NiveliStudimeve).ToListAsync();

            return Ok(aplikimetEReja);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("ShfaqAplikiminNgaID")]
        public async Task<IActionResult> ShfaqAplikiminNgaID(int id)
        {
            var aplikimetEReja = await _context.AplikimetEReja.Include(x => x.Departamentet).ThenInclude(x => x.TarifatDepartamenti).Include(x => x.NiveliStudimeve).Include(x => x.Zbritja).Where(x => x.AplikimiID == id).FirstOrDefaultAsync();

            var tarifa = await _context.TarifatDepartamenti.Where(x => x.DepartamentiID == aplikimetEReja.DepartamentiID && x.NiveliStudimitID == aplikimetEReja.NiveliStudimitID).FirstOrDefaultAsync();

            AdministrataModel modeli = new()
            {
                AplikimetEReja = aplikimetEReja,
                TarifaDepartamenti = tarifa,
            };

            return Ok(modeli);
        }

        [Authorize]
        [HttpGet]
        [Route("ShfaqLlogariteBankare")]
        public async Task<IActionResult> ShfaqLlogariteBankare()
        {
            var bankat = await _context.Bankat.Where(x => x.LlojiBankes != "Dalese" && x.LlojiBankes != "TarifatEStudentit").ToListAsync();

            return Ok(bankat);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("ShfaqStudentet")]
        public async Task<IActionResult> ShfaqStudentet()
        {
            var perdoruesit = await _context.Perdoruesit.Include(x => x.TeDhenatPerdoruesit)
                .ToListAsync();

            var perdoruesiList = new List<Perdoruesi>();

            foreach (var perdoruesi in perdoruesit)
            {
                var user = await UserManager.FindByIdAsync(perdoruesi.AspNetUserId);
                var roles = (await UserManager.GetRolesAsync(user)).Where(role => role != "User").ToList();

                var eshteStudent = await UserManager.IsInRoleAsync(user, "Student");

                if (eshteStudent == true)
                {
                    var personiNgaKerkimi = await _context.Perdoruesit
                        .Include(x => x.TeDhenatPerdoruesit)
                        .Include(x => x.TeDhenatRegjistrimitStudentit)
                        .ThenInclude(x => x.Departamentet)
                        .Include(x => x.TeDhenatRegjistrimitStudentit)
                        .ThenInclude(x => x.NiveliStudimeve)
                        .Where(x => x.AspNetUserId == perdoruesi.AspNetUserId)
                        .FirstOrDefaultAsync();

                    perdoruesiList.Add(personiNgaKerkimi);
                }
            }

            return Ok(perdoruesiList);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpPut]
        [Route("PerditesoniTeDhenatStudentit")]
        public async Task<IActionResult> PerditesoniTeDhenatStudentit(string studentiID, [FromBody] AdministrataModel studenti)
        {
            var studentiKerkim = await _context.Perdoruesit.Where(x => x.AspNetUserId == studentiID).FirstOrDefaultAsync();
            var teDhenatStudentitKerkim = await _context.TeDhenatPerdoruesit.Where(x => x.UserID == studentiKerkim.UserID).FirstOrDefaultAsync();

            if (studentiKerkim == null || teDhenatStudentitKerkim == null)
            {
                return BadRequest("Ky student nuk u gjet!");
            }

            studentiKerkim.Emri = studenti.Perdoruesi.Emri;
            studentiKerkim.Mbiemri = studenti.Perdoruesi.Mbiemri;
            studentiKerkim.AspNetUserId = studenti.Perdoruesi.AspNetUserId;

            _context.Perdoruesit.Update(studentiKerkim);
            await _context.SaveChangesAsync();

            teDhenatStudentitKerkim.NrKontaktit = studenti.TeDhenatPerdoruesit.NrKontaktit;
            teDhenatStudentitKerkim.Adresa = studenti.TeDhenatPerdoruesit.Adresa;
            teDhenatStudentitKerkim.DataLindjes = studenti.TeDhenatPerdoruesit.DataLindjes;
            teDhenatStudentitKerkim.Qyteti = studenti.TeDhenatPerdoruesit?.Qyteti;
            teDhenatStudentitKerkim.Shteti = studenti.TeDhenatPerdoruesit?.Shteti;
            teDhenatStudentitKerkim.ZipKodi = studenti.TeDhenatPerdoruesit?.ZipKodi;
            teDhenatStudentitKerkim.EmriPrindit = studenti.TeDhenatPerdoruesit?.EmriPrindit;
            teDhenatStudentitKerkim.EmailPersonal = studenti.TeDhenatPerdoruesit?.EmailPersonal;
            teDhenatStudentitKerkim.UserID = teDhenatStudentitKerkim.UserID;

            _context.TeDhenatPerdoruesit.Update(teDhenatStudentitKerkim);
            await _context.SaveChangesAsync();

            return Ok(studenti);
        }

        /*[Authorize(Policy = "eshteStaf")]*/
        [AllowAnonymous]
        [HttpGet]
        [Route("ShfaqDetajetVertetimiStudentore")]
        public async Task<IActionResult> ShfaqDetajetVertetimiStudentore(int userID)
        {
            var studenti = await _context.Perdoruesit
                .Include(x => x.TeDhenatPerdoruesit)
                .Include(x => x.ParaqitjaSemestrit)
                    .ThenInclude(x => x.Semestri)
                .Include(x => x.TeDhenatRegjistrimitStudentit)
                    .ThenInclude(x => x.Departamentet)
                .Include(x => x.TeDhenatRegjistrimitStudentit)
                    .ThenInclude(x => x.NiveliStudimeve)
                        .ThenInclude(x => x.Semestrat)
                .Where(x => x.UserID == userID)
                .OrderByDescending(x => x.ParaqitjaSemestrit.ParaqitjaSemestritID)
                .FirstOrDefaultAsync();


            return Ok(studenti);
        }
    }

}
