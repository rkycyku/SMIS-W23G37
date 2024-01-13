using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using W23G37.Data;
using W23G37.Models;

namespace W23G37.Areas.Financat.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class FinancatController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<IdentityUser> UserManager;

        public FinancatController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            UserManager = userManager;
        }

        public class FinancatModel
        {
            public virtual Bankat? Banka { get; set; }
            public virtual Pagesat? Pagesa { get; set; }
            public virtual List<Departamentet>? Departamentet { get; set; }
            public virtual Departamentet? Departamenti { get; set; }
            public virtual List<TarifatDepartamenti>? TarifatDepartamentiList { get; set; }
            public virtual TarifatDepartamenti? TarifatDepartamenti { get; set; }
        }

        public class Personi
        {
            public int? ID { get; set; }
            public string? Emri { get; set; }
            public string? Mbiemri { get; set; }
            public string? KodiFinanciar { get; set; }
            public string? Username { get; set; }
            public string? LlojiPersonit { get; set; }
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("ShfaqBankat")]
        public async Task<IActionResult> ShfaqBankat()
        {
            var bankat = await _context.Bankat.ToListAsync();

            return Ok(bankat);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("ShfaqBankenNgaID")]
        public async Task<IActionResult> ShfaqBankenNgaID(int bankaID)
        {
            var banka = await _context.Bankat.Where(x => x.BankaID == bankaID).FirstOrDefaultAsync();

            if (banka == null)
            {
                return BadRequest("Nuk egziston asnje Banke!");
            }

            return Ok(banka);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpPost]
        [Route("ShtoniBanken")]
        public async Task<IActionResult> ShtoniBanken([FromBody] FinancatModel banka)
        {
            await _context.Bankat.AddAsync(banka.Banka);
            await _context.SaveChangesAsync();

            return Ok(banka);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpDelete]
        [Route("FshiniBanken")]
        public async Task<IActionResult> FshiniBanken(int bankaID)
        {
            var banka = await _context.Bankat.Where(x => x.BankaID == bankaID).FirstOrDefaultAsync();

            if (banka == null)
            {
                return BadRequest("Nuk egziston asnje Banke!");
            }

            _context.Bankat.Remove(banka);
            await _context.SaveChangesAsync();

            return Ok(banka);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpPut]
        [Route("PerditesoniBanken")]
        public async Task<IActionResult> PerditesoniBanken(int bankaID, [FromBody] FinancatModel banka)
        {
            var bankaKerkim = await _context.Bankat.Where(x => x.BankaID == bankaID).FirstOrDefaultAsync();

            if (bankaKerkim == null)
            {
                return BadRequest("Nuk egziston kjo banke!");
            }

            bankaKerkim.EmriBankes = banka.Banka.EmriBankes;
            bankaKerkim.KodiBankes = banka.Banka.KodiBankes;
            bankaKerkim.NumriLlogaris = banka.Banka.NumriLlogaris;
            bankaKerkim.AdresaBankes = banka.Banka.AdresaBankes;
            bankaKerkim.BicKodi = banka.Banka.BicKodi;
            bankaKerkim.SwiftKodi = banka.Banka.SwiftKodi;
            bankaKerkim.Valuta = banka.Banka.Valuta;
            bankaKerkim.IbanFillimi = banka.Banka.IbanFillimi;
            bankaKerkim.LlojiBankes = banka.Banka.LlojiBankes;

            _context.Bankat.Update(bankaKerkim);
            await _context.SaveChangesAsync();

            return Ok(banka);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("ShfaqniPagesat")]
        public async Task<IActionResult> ShfaqniPagesat()
        {
            var pagesat = await _context.Pagesat.Include(x => x.Bankat).Include(x => x.AplikimetEReja).Include(x => x.Perdoruesi).ThenInclude(x => x.TeDhenatRegjistrimitStudentit).ToListAsync();

            return Ok(pagesat);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("ShfaqniPersonatPerPagese")]
        public async Task<IActionResult> ShfaqniPersonatPerPagese()
        {
            var perdoruesit = await _context.Perdoruesit.Include(x => x.TeDhenatRegjistrimitStudentit).ToListAsync();
            var aplikmetEReja = await _context.AplikimetEReja.ToListAsync();

            var personat = new List<Personi>();

            foreach (var p in perdoruesit)
            {
                Personi personi = new()
                {
                    ID = p.UserID,
                    Emri = p.Emri,
                    Mbiemri = p.Mbiemri,
                    Username = p.Username,
                    KodiFinanciar = p.TeDhenatRegjistrimitStudentit?.KodiFinanciar,
                    LlojiPersonit = "Perdorues"
                };

                personat.Add(personi);
            }

            foreach (var a in aplikmetEReja)
            {
                Personi personi = new()
                {
                    ID = a.AplikimiID,
                    Emri = a.Emri,
                    Mbiemri = a.Mbiemri,
                    Username = null,
                    KodiFinanciar = a.KodiFinanciar,
                    LlojiPersonit = "AplimiIRi"
                };

                personat.Add(personi);
            }

            return Ok(personat);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpPost]
        [Route("ShtoniPagesen")]
        public async Task<IActionResult> ShtoniPagesen([FromBody] FinancatModel pagesa)
        {
            await _context.Pagesat.AddAsync(pagesa.Pagesa);
            await _context.SaveChangesAsync();

            return Ok(pagesa);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpDelete]
        [Route("FshiniPagesen")]
        public async Task<IActionResult> FshiniPagesen(int pagesaID)
        {
            var pagesa = await _context.Pagesat.Where(x => x.PagesaID == pagesaID).FirstOrDefaultAsync();

            if (pagesa == null)
            {
                return BadRequest("Nuk egziston asnje Pagese me kete numer!");
            }

            _context.Pagesat.Remove(pagesa);
            await _context.SaveChangesAsync();

            return Ok("Pagesa u fshi me sukses!");
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("ShfaqniDepartamentet")]
        public async Task<IActionResult> ShfaqniDepartamentet()
        {
            var departamentet = await _context.Departamentet.Include(x => x.TarifatDepartamenti).ToListAsync();

            FinancatModel modeli = new()
            {
                Departamentet = departamentet.ToList(),
            };

            return Ok(modeli);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("ShfaqniTarifatPerDepartamentin")]
        public async Task<IActionResult> ShfaqniTarifatPerDepartamentin(int DepartamentiID)
        {
            var departamenti = await _context.Departamentet.Include(x => x.TarifatDepartamenti).ThenInclude(x => x.NiveliStudimeve).Where(x => x.DepartamentiID == DepartamentiID).FirstOrDefaultAsync();

            FinancatModel modeli = new()
            {
                Departamenti = departamenti,
            };

            return Ok(modeli);
        }

        [AllowAnonymous]
        /*[Authorize(Policy = "eshteStaf")]*/
        [HttpGet]
        [Route("ShfaqniDetajetTarifes")]
        public async Task<IActionResult> ShfaqniDetajetTarifes(int TarifaID)
        {
            var tarifa = await _context.TarifatDepartamenti.Include(x => x.NiveliStudimeve).Where(x => x.TarifaID == TarifaID).FirstOrDefaultAsync();

            if (tarifa == null)
            {
                return BadRequest("Kjo tarife nuk u gjet!");
            }

            return Ok(tarifa);
        }

        /*[Authorize(Policy = "eshteStaf")]*/
        [AllowAnonymous]
        [HttpPut]
        [Route("PerditesoniTarifenDepartamentit")]
        public async Task<IActionResult> PerditesoniTarifenDepartamentit(int tarifaID, [FromBody] FinancatModel tarifatDepartamenti)
        {
            var tarifaKerkim = await _context.TarifatDepartamenti.Include(x => x.NiveliStudimeve).Where(x => x.TarifaID == tarifaID).FirstOrDefaultAsync();

            if (tarifaKerkim == null)
            {
                return BadRequest("Kjo tarife nuk u gjet!");
            }

            tarifaKerkim.TarifaVjetore = tarifatDepartamenti.TarifatDepartamenti.TarifaVjetore;

            _context.TarifatDepartamenti.Update(tarifaKerkim);
            await _context.SaveChangesAsync();

            return Ok(tarifatDepartamenti);
        }

        /*[Authorize(Policy = "eshteStaf")]*/
        [AllowAnonymous]
        [HttpGet]
        [Route("ShfaqStudentet")]
        public async Task<IActionResult> ShfaqStudentet()
        {
            var perdoruesit = await _context.Perdoruesit.Include(x => x.TeDhenatPerdoruesit)
                .ToListAsync();

            var perdoruesiList = new List<Perdoruesi>();

            List<Object> studentetList = new();

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

                    var TarifaStudentit = await _context.TarifaStudenti.Include(x => x.Zbritja1).Include(x => x.Zbritja2).Where(x => x.StudentiID == personiNgaKerkimi.UserID).FirstOrDefaultAsync();

                    var TarifaDepartamentit = await _context.TarifatDepartamenti
                        .Where(x => x.DepartamentiID == personiNgaKerkimi.TeDhenatRegjistrimitStudentit.DepartamentiID && x.NiveliStudimitID
                         == personiNgaKerkimi.TeDhenatRegjistrimitStudentit.NiveliStudimitID)
                        .FirstOrDefaultAsync();

                    var pagesat = await _context.Pagesat.Where(x => x.PersoniID == personiNgaKerkimi.UserID).ToListAsync();

                    Object studenti = new
                    {

                        Studenti = new
                        {
                            AspNetUserID = personiNgaKerkimi.AspNetUserId,
                            Studenti = $"{personiNgaKerkimi.Emri} {personiNgaKerkimi.TeDhenatPerdoruesit.EmriPrindit} {personiNgaKerkimi.Mbiemri}",
                            IDStudentiFK = personiNgaKerkimi.TeDhenatRegjistrimitStudentit.IdStudenti,
                            KodiFinanciar = personiNgaKerkimi.TeDhenatRegjistrimitStudentit.KodiFinanciar,
                        },
                        TeDhenatRegjistrimit = new
                        {
                            Departamenti = personiNgaKerkimi.TeDhenatRegjistrimitStudentit.Departamentet.EmriDepartamentit,
                            ShkurtesaDepartamentit = personiNgaKerkimi.TeDhenatRegjistrimitStudentit.Departamentet.ShkurtesaDepartamentit,
                            NiveliStudimeve = personiNgaKerkimi.TeDhenatRegjistrimitStudentit.NiveliStudimeve.EmriNivelitStudimeve,
                            ShkurtesaNivelitStudimeve = personiNgaKerkimi.TeDhenatRegjistrimitStudentit.NiveliStudimeve.ShkurtesaEmritNivelitStudimeve,
                        },
                        PagesatDheTarifa = new
                        {
                            TotPagesat = pagesat.Where(x => x.Pagesa != null).Sum(x => double.Parse(x.Pagesa)),
                            TotTarifat = pagesat.Where(x => x.KestiPageses != null).Sum(x => double.Parse(x.KestiPageses)),
                            TarifaDepartamentit =
                            TarifaStudentit.TarifaFikse == null ? TarifaDepartamentit.TarifaVjetore - (TarifaDepartamentit.TarifaVjetore * (TarifaStudentit.Zbritja1?.Zbritja / 100)) - (TarifaDepartamentit.TarifaVjetore * (TarifaStudentit.Zbritja2 != null ? TarifaStudentit.Zbritja2.Zbritja / 100 : 0))
                            :
                            TarifaStudentit.TarifaFikse - (TarifaStudentit.TarifaFikse * (TarifaStudentit.Zbritja1?.Zbritja / 100)) - (TarifaStudentit.TarifaFikse * (TarifaStudentit.Zbritja2 != null ? TarifaStudentit.Zbritja2.Zbritja / 100 : 0)),
                            Zbritja1 = new
                            {
                                EmriZbritjes = TarifaStudentit.Zbritja1?.EmriZbritjes,
                                Zbritja = TarifaStudentit.Zbritja1?.Zbritja
                            },
                            Zbritja2 = new
                            {
                                EmriZbritjes = TarifaStudentit.Zbritja2?.EmriZbritjes,
                                Zbritja = TarifaStudentit.Zbritja2?.Zbritja
                            },
                        }
                    };

                    perdoruesiList.Add(personiNgaKerkimi);

                    studentetList.Add(studenti);
                }
            }

            return Ok(studentetList);
        }

        /*[Authorize(Policy = "eshteStaf")]*/
        [AllowAnonymous]
        [HttpPut]
        [Route("PerditesoniTarifenStudentit")]
        public async Task<IActionResult> PerditesoniTarifenStudentit(string StudentiID)
        {
            var perdoruesi = await _context.Perdoruesit.Where(x => x.AspNetUserId == StudentiID).FirstOrDefaultAsync();

            var tarifaKerkim = await _context.TarifaStudenti.Where(x => x.StudentiID == perdoruesi.UserID).FirstOrDefaultAsync();

            if (tarifaKerkim == null)
            {
                return BadRequest("Kjo tarife nuk u gjet!");
            }

            tarifaKerkim.Zbritja2ID = 1;

            _context.TarifaStudenti.Update(tarifaKerkim);
            await _context.SaveChangesAsync();

            return Ok(tarifaKerkim);
        }
    }
}
