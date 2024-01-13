using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
using W23G37.Data;
using W23G37.Models;
using static W23G37.Areas.Studentet.Controllers.StudentetController;

namespace W23G37.Areas.Profesor.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProfesorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public class ProfesorModel
        {
            public List<LendetDepartamentiProfesori>? LDPList { get; set; }
            public List<Lendet>? Lendet { get; set; }
            public List<ParaqitjaProvimit>? ParaqitjaProvimit { get; set; }

            public List<Departamentet>? Departamentet { get; set; }
            public List<Provimi>? Provimet { get; set; }
        }

        public class Provimi
        {
            public int? ParaqitjaProvimitID { get; set; }
            public int? StudentiID { get; set; }
            public string? Studenti { get; set; }
            public string? StudentiIDFK { get; set; }
            public string? Nota { get; set; }
            public string? StatusiNotes { get; set; }
        }

        /*[Authorize]*/
        [AllowAnonymous]
        [HttpGet]
        [Route("ShfaqLendetProfesorit")]
        public async Task<IActionResult> ShfaqLendetProfesorit(string profesoriID)
        {
            var perdoruesi = await _context.Perdoruesit.Where(x => x.AspNetUserId.Equals(profesoriID)).FirstOrDefaultAsync();
            var ldp = await _context.LendetDepartamentiProfesori.Include(x => x.Lendet).Where(x => x.ProfesoriID == perdoruesi.UserID).ToListAsync();

            var lendet = new List<Lendet>();

            foreach (var lendaNeLDP in ldp)
            {
                var lenda = lendaNeLDP.Lendet;

                if (!lendet.Contains(lenda))
                {
                    lendet.Add(lenda);
                }
            }

            ProfesorModel modeli = new()
            {
                Lendet = lendet
            };

            return Ok(modeli);
        }

        /*[Authorize]*/
        [AllowAnonymous]
        [HttpGet]
        [Route("ShfaqDepartamentinPerLendetProfesorit")]
        public async Task<IActionResult> ShfaqDepartamentinPerLendetProfesorit(string profesoriID, int lendaID)
        {
            var perdoruesi = await _context.Perdoruesit.Where(x => x.AspNetUserId.Equals(profesoriID)).FirstOrDefaultAsync();
            var ldp = await _context.LendetDepartamentiProfesori.Include(x => x.Departamentet).Include(x => x.Lendet).Where(x => x.ProfesoriID == perdoruesi.UserID && x.LendaID == lendaID).ToListAsync();

            var departamentet = new List<Departamentet>();

            foreach (var lendaNeLDP in ldp)
            {
                var lenda = lendaNeLDP.Departamentet;

                if (!departamentet.Contains(lenda))
                {
                    departamentet.Add(lenda);
                }
            }

            ProfesorModel modeli = new()
            {
                Departamentet = departamentet
            };

            return Ok(modeli);
        }

        /*[Authorize]*/
        [AllowAnonymous]
        [HttpGet]
        [Route("ShfaqStudentetQeKaneParaqiturProvimin")]
        public async Task<IActionResult> ShfaqStudentetQeKaneParaqiturProvimin(string profesoriID, int lendaID, int departamentiID)
        {
            var perdoruesi = await _context.Perdoruesit.Where(x => x.AspNetUserId.Equals(profesoriID)).FirstOrDefaultAsync();
            var appIFundit = await _context.AfatiParaqitjesProvimit.OrderByDescending(x => x.APPID).FirstOrDefaultAsync();

            var paraqitjaProvimit = await _context.ParaqitjaProvimit.Include(x => x.Studenti).ThenInclude(x => x.TeDhenatRegjistrimitStudentit)
                .Where(x => x.LendetDepartamentiProfesori.LendaID == lendaID && x.LendetDepartamentiProfesori.DepartamentiID == departamentiID && x.LendetDepartamentiProfesori.ProfesoriID == perdoruesi.UserID && (x.Nota == null || x.APPID == appIFundit.APPID)).ToListAsync();

            List<Provimi> provimet = new();

            foreach (var p in paraqitjaProvimit)
            {
                Provimi provimi = new()
                {
                    ParaqitjaProvimitID = p.ParaqitjaProvimitID,
                    StudentiID = p.StudentiID,
                    Studenti = (p.Studenti?.Emri + " " + p.Studenti?.Mbiemri).ToString(),
                    StudentiIDFK = p.Studenti.TeDhenatRegjistrimitStudentit.IdStudenti,
                    Nota = p.Nota,
                    StatusiNotes = p.StatusiINotes
                };

                provimet.Add(provimi);
            }

            return Ok(provimet);
        }

        /*[Authorize]*/
        [AllowAnonymous]
        [HttpPost]
        [Route("VendosNotenStudentit")]
        public async Task<IActionResult> VendosNotenStudentit(int paraqitjaProvimitID, int nota, string statusi)
        {
            var paraqitjaProvimit = await _context.ParaqitjaProvimit.Include(x => x.LendetDepartamentiProfesori).Where(x => x.ParaqitjaProvimitID == paraqitjaProvimitID).FirstOrDefaultAsync();

            if (paraqitjaProvimit == null)
            {
                return BadRequest();
            }

            paraqitjaProvimit.Nota = nota.ToString();
            paraqitjaProvimit.StatusiINotes = statusi;
            paraqitjaProvimit.DataVendosjesSeNotes = DateTime.Now;

            _context.ParaqitjaProvimit.Update(paraqitjaProvimit);
            await _context.SaveChangesAsync();

            if (nota > 5)
            {
                NotatStudenti notaStudenti = new()
                {
                    LendaID = paraqitjaProvimit.LendetDepartamentiProfesori.LendaID,
                    ParaqitjaProvimitID = paraqitjaProvimitID,
                    Nota = nota,
                    StudentiID = paraqitjaProvimit.StudentiID
                };

                await _context.NotatStudenti.AddAsync(notaStudenti);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        /*[Authorize]*/
        [AllowAnonymous]
        [HttpPut]
        [Route("PerditesoNotenStudentit")]
        public async Task<IActionResult> PerditesoNotenStudentit(int paraqitjaProvimitID, int nota, string statusi)
        {
            var paraqitjaProvimit = await _context.ParaqitjaProvimit.Include(x => x.LendetDepartamentiProfesori).Where(x => x.ParaqitjaProvimitID == paraqitjaProvimitID).FirstOrDefaultAsync();

            if (paraqitjaProvimit == null)
            {
                return BadRequest();
            }

            paraqitjaProvimit.Nota = nota.ToString();
            paraqitjaProvimit.StatusiINotes = statusi;
            paraqitjaProvimit.DataVendosjesSeNotes = DateTime.Now;

            _context.ParaqitjaProvimit.Update(paraqitjaProvimit);
            await _context.SaveChangesAsync();

            if (nota > 5)
            {
                var notaStudenti = await _context.NotatStudenti.Where(x => x.ParaqitjaProvimitID == paraqitjaProvimitID).FirstOrDefaultAsync();
                if (notaStudenti == null)
                {
                    NotatStudenti n = new()
                    {
                        LendaID = paraqitjaProvimit.LendetDepartamentiProfesori.LendaID,
                        ParaqitjaProvimitID = paraqitjaProvimitID,
                        Nota = nota,
                        StudentiID = paraqitjaProvimit.StudentiID
                    };

                    await _context.NotatStudenti.AddAsync(n);
                    await _context.SaveChangesAsync();

                    return Ok();
                };

                notaStudenti.Nota = nota;
                notaStudenti.DataVendosjesSeNotes = DateTime.Now;

                _context.NotatStudenti.Update(notaStudenti);
                await _context.SaveChangesAsync();
            }
            else
            {
                var notaStudenti = await _context.NotatStudenti.Where(x => x.ParaqitjaProvimitID == paraqitjaProvimitID).FirstOrDefaultAsync();
                if (notaStudenti == null)
                {
                    return Ok();
                };

                _context.NotatStudenti.Remove(notaStudenti);
                await _context.SaveChangesAsync();
            }

            return Ok();
        }

        /*[Authorize]*/
        [AllowAnonymous]
        [HttpPut]
        [Route("RefuzoNotenStudentit")]
        public async Task<IActionResult> RefuzoNotenStudentit(int paraqitjaProvimitID)
        {
            var paraqitjaProvimit = await _context.ParaqitjaProvimit.Include(x => x.LendetDepartamentiProfesori).Where(x => x.ParaqitjaProvimitID == paraqitjaProvimitID).FirstOrDefaultAsync();

            if (paraqitjaProvimit == null)
            {
                return BadRequest();
            }

            paraqitjaProvimit.Nota = "Nota eshte Refuzuar";
            paraqitjaProvimit.StatusiINotes = "ERefuzuar";
            paraqitjaProvimit.DataVendosjesSeNotes = DateTime.Now;

            _context.ParaqitjaProvimit.Update(paraqitjaProvimit);
            await _context.SaveChangesAsync();

            var notaStudenti = await _context.NotatStudenti.Where(x => x.ParaqitjaProvimitID == paraqitjaProvimitID).FirstOrDefaultAsync();

            if (notaStudenti == null)
            {
                return Ok();
            }
            else
            {
                _context.NotatStudenti.Remove(notaStudenti);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }
    }
}
