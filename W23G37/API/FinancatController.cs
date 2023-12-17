using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using W23G37.Data;
using W23G37.Models;

namespace W23G37.API
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class FinancatController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FinancatController(ApplicationDbContext context)
        {
            _context = context;
        }

        public class FinancatModel
        {
            public virtual Bankat? Banka { get; set; }
            public virtual Pagesat? Pagesa { get; set; }
        }

        public class Personi
        {
            public int? ID { get; set; }
            public string? Emri { get; set; }
            public string? Mbiemri { get; set; }
            public string? KodiFinanciar { get; set; }
            public string? Username {  get; set; }
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

            foreach(var p in perdoruesit)
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
    }
}
