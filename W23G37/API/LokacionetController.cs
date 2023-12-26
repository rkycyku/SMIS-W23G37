using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using W23G37.Data;
using W23G37.Models;
using static W23G37.Controllers.LokacionetController;

namespace W23G37.API
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/[controller]")]
    public class LokacionetController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LokacionetController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("shfaqLokacionet")]
        public async Task<IActionResult> ShfaqLokacionet()
        {
            var lokacionet = await _context.Lokacionet.ToListAsync();

            return Ok(lokacionet);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("shfaqLokacioninNgaID")]
        public async Task<IActionResult> ShfaqLokacioninNgaID(int id)
        {
            var lokacioni = await _context.Lokacionet.Where(x => x.LokacioniID == id).FirstOrDefaultAsync();

            if (lokacioni == null)
            {
                return BadRequest("Lokacioni nuk egziston");
            }


            return Ok(lokacioni);
        }


        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("shfaqLokacionetSelektim")]
        public async Task<IActionResult> ShfaqLokacionetSelektim()
        {
            var lokacionet = await _context.Lokacionet.ToListAsync();

            if (lokacionet == null)
            {
                return BadRequest("Lokacioni nuk egziston ose nuk ka salla!");
            }

           var lokacionetList = new List<Lokacionet>();

            foreach (var lokacioni in lokacionet)
            {
                var lokacioniJson = new Lokacionet
                {
                    LokacioniID = lokacioni.LokacioniID,
                    EmriLokacionit = lokacioni.EmriLokacionit
                };

                lokacionetList.Add(lokacioniJson);
            }

            var sallaJson = new LokacionetViewModel
            {
                Lokacionet = lokacionetList,
            };

            return Ok(sallaJson);
        }

        //Sallat
        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("shfaqSallat")]
        public async Task<IActionResult> ShfaqSallat()
        {
            var sallat = await _context.Sallat.Include(x => x.Lokacioni).ToListAsync();

            var sallatList = new List<LokacionetViewModel>();

            foreach (var salla in sallat)
            {
                var sallaJson = new LokacionetViewModel
                {
                    Salla = salla,
                    Lokacioni = salla.Lokacioni,
                };

                sallatList.Add(sallaJson);
            }

            return Ok(sallatList);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("shfaqSallatNgaID")]
        public async Task<IActionResult> ShfaqSallatNgaID(int id)
        {
            var salla = await _context.Sallat.Include(x => x.Lokacioni).Where(x => x.SallaID == id).FirstOrDefaultAsync();

            if (salla == null)
            {
                return BadRequest("Salla nuk egziston");
            }

            var sallaJson = new LokacionetViewModel
            {
                Salla = salla,
                Lokacioni = salla.Lokacioni,
            };

            return Ok(sallaJson);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("shfaqSallatPerLokacionin")]
        public async Task<IActionResult> ShfaqSallatPerLokacionin(int LokacioniID)
        {
            var sallat = await _context.Sallat.Include(x => x.Lokacioni).Where(x => x.LokacioniID == LokacioniID).ToListAsync();

            if (sallat == null)
            {
                return BadRequest("Lokacioni nuk egziston ose nuk ka salla!");
            }

            var sallatList = new List<LokacionetViewModel>();

            foreach (var salla in sallat)
            {
                var sallaJson = new LokacionetViewModel
                {
                    Salla = salla,
                    Lokacioni = salla.Lokacioni,
                };

                sallatList.Add(sallaJson);
            }

            return Ok(sallatList);
        }


        [Authorize(Policy = "eshteStaf")]
        [HttpPost]
        [Route("shtoLokacionin")]
        public async Task<IActionResult> ShtoLokacionin([FromBody] Lokacionet lokacionet)
        {
            await _context.Lokacionet.AddAsync(lokacionet);
            await _context.SaveChangesAsync();

            return Ok(lokacionet);
        }


        [Authorize(Policy = "eshteStaf")]
        [HttpPost]
        [Route("shtoSallen")]
        public async Task<IActionResult> ShtoSallen([FromBody] Sallat s)
        {
            

            var lokacioni = await _context.Lokacionet.Where(x => x.LokacioniID == s.LokacioniID).FirstOrDefaultAsync();
            var sallatCount = await _context.Sallat.Where(x => x.LokacioniID == s.LokacioniID).CountAsync();

            if (lokacioni == null)
            {
                return BadRequest("Lokacioni ose Salla nuk egzistojne");
            }

            var kodiSalles = $"{lokacioni.ShkurtesaLokacionit.ToString().ToUpper()}{lokacioni.LokacioniID:D2}{sallatCount + 1:D3}";

            var salla = new Sallat
            {
                KapacitetiSalles = s.KapacitetiSalles,
                LokacioniID = s.LokacioniID,
                KodiSalles = kodiSalles
            };

            await _context.Sallat.AddAsync(salla);
            await _context.SaveChangesAsync();

            return Ok(salla);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpDelete]
        [Route("fshiniSallen")]
        public async Task<IActionResult> FshiniSallen(int id)
        {
            var salla = await _context.Sallat.Include(x => x.Lokacioni).Where(x => x.SallaID == id).FirstOrDefaultAsync();

            if (salla == null)
            {
                return BadRequest("Salla nuk egziston");
            }

            _context.Sallat.Remove(salla);
            await _context.SaveChangesAsync();


            return Ok("Lokacioni u fshi me sukses!");
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpDelete]
        [Route("fshiniLokacionin")]
        public async Task<IActionResult> FshiniLokacionin(int id)
        {
            var lokacioni = await _context.Lokacionet.Where(x => x.LokacioniID == id).FirstOrDefaultAsync();

            if (lokacioni == null)
            {
                return BadRequest("Lokacioni nuk egziston");
            }

            _context.Lokacionet.Remove(lokacioni);
            await _context.SaveChangesAsync();


            return Ok("Lokacioni u fshi me sukses!");
        }

    }
}
