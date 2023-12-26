using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using W23G37.Data;
using W23G37.Models;

namespace W23G37.Areas.Administrata.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrataController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AdministrataController(ApplicationDbContext context)
        {
            _context = context;
        }

        public class AdministrataModel
        {
            public virtual AplikimetEReja? AplikimetEReja { get; set; }
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
        [Route("shfaqNiveletStudimitGjatAplikimit")]
        public async Task<IActionResult> ShfaqNiveletStudimitGjatAplikimit(int departamentiID)
        {
            var niveletEStudimit = await _context.NiveliStudimitDepartamenti.Include(x => x.NiveliStudimeve).Include(x => x.Departamentet).Where(x => x.DepartamentiID == departamentiID).ToListAsync();

            return Ok(niveletEStudimit);
        }

        /*[Authorize(Policy = "eshteStaf")]*/
        [AllowAnonymous]
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
    }

}
