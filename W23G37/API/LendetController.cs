using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using W23G37.Data;
using W23G37.Models;

namespace W23G37.API
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class LendetController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public LendetController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("shfaqLendet")]
        public async Task<IActionResult> ShfaqLendet()
        {
            var lendet = await _context.Lendet.ToListAsync();

            return Ok(lendet);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("shfaqLendenNgaID")]
        public async Task<IActionResult> ShfaqLendenNgaID(int id)
        {
            var lenda = await _context.Lendet.Where(x => x.LendaID == id).FirstOrDefaultAsync();

            if (lenda == null)
            {
                return BadRequest("Lenda nuk egziston!");
            }

            return Ok(lenda);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpPost]
        [Route("shtoLenden")]
        public async Task<IActionResult> ShtoLenden([FromBody] Lendet l)
        {
            var lendetCount = await _context.Lendet.CountAsync();

            var kodiLendes = $"{l.SemestriLigjerimit:D2}{l.ShkurtesaLendes.ToString().ToUpper()}{lendetCount + 1:D3}";

            var lenda = new Lendet
            {
                KodiLendes = kodiLendes,
                EmriLendes = l.EmriLendes,
                ShkurtesaLendes = l.ShkurtesaLendes.ToString().ToUpper(),
                KategoriaLendes = l.KategoriaLendes,
                KreditELendes = l.KreditELendes,
                SemestriLigjerimit = l.SemestriLigjerimit,
            };

            await _context.Lendet.AddAsync(lenda);
            await _context.SaveChangesAsync();

            return Ok(lenda);
        }
    }
}
