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
    public class DepartamentetController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DepartamentetController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("shfaqDepartamentet")]
        public async Task<IActionResult> ShfaqDepartmanetet()
        {
            var departamentet = await _context.Departamentet.ToListAsync();

            return Ok(departamentet);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("shfaqDepartamentinNgaID")]
        public async Task<IActionResult> ShfaqDepartamentinNgaID(int id)
        {
            var departamenti = await _context.Departamentet.Where(x => x.DepartamentiID == id).FirstOrDefaultAsync();

            if (departamenti == null)
            {
                return BadRequest("Departamenti nuk egziston!");
            }

            return Ok(departamenti);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpPost]
        [Route("shtoDepartamentin")]
        public async Task<IActionResult> ShtoDepartamentin([FromBody] Departamentet departamentet)
        {
            await _context.Departamentet.AddAsync(departamentet);
            await _context.SaveChangesAsync();

            return Ok(departamentet);
        }
    }
}
