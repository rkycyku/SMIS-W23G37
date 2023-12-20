﻿

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using W23G37.Data;
using W23G37.Models;

namespace W23G37.API
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/[controller]")]
    public class PerdoruesiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public PerdoruesiController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("shfaqPerdoruesit")]
        public async Task<IActionResult> shfaqPerdoruesit()
        {
            var perdoruesit = await _context.Perdoruesit.Include(x => x.TeDhenatPerdoruesit)
                .ToListAsync();

            var perdoruesiList = new List<PerdoruesiJsonKthimi>();

            foreach (var perdoruesi in perdoruesit)
            {
                var user = await _userManager.FindByIdAsync(perdoruesi.AspNetUserId);
                var roles = await _userManager.GetRolesAsync(user);

                var eshteStudent = await _userManager.IsInRoleAsync(user, "Student");

                if (eshteStudent == false)
                {

                    var personiNgaKerkimi = await _context.Perdoruesit.Include(x => x.TeDhenatPerdoruesit).Where(x => x.AspNetUserId == perdoruesi.AspNetUserId).FirstOrDefaultAsync();


                    var perdoruesiJson = new PerdoruesiJsonKthimi
                    {
                        UserID = personiNgaKerkimi.UserID,
                        Emri = personiNgaKerkimi.Emri,
                        Mbiemri = personiNgaKerkimi.Mbiemri,
                        Email = personiNgaKerkimi.Email,
                        Username = personiNgaKerkimi.Username,
                        AspNetUserId = personiNgaKerkimi.AspNetUserId,
                        TeDhenatID = personiNgaKerkimi.TeDhenatPerdoruesit.TeDhenatID,
                        NrKontaktit = personiNgaKerkimi.TeDhenatPerdoruesit.NrKontaktit,
                        Qyteti = personiNgaKerkimi.TeDhenatPerdoruesit.Qyteti,
                        ZipKodi = personiNgaKerkimi.TeDhenatPerdoruesit.ZipKodi,
                        Adresa = personiNgaKerkimi.TeDhenatPerdoruesit.Adresa,
                        Shteti = personiNgaKerkimi.TeDhenatPerdoruesit.Shteti,
                        Rolet = roles.ToList()
                    };

                    perdoruesiList.Add(perdoruesiJson);
                }
            }

            return Ok(perdoruesiList);
        }

        /*[Authorize(Policy = "eshteStaf")]*/
        [AllowAnonymous]
        [HttpGet]
        [Route("editoRoletPerdoruesit")]
        public async Task<IActionResult> editoRoletPerdoruesit(string AspNetUserId, string llojiEditmit)
        {
            var user = await _userManager.FindByIdAsync(AspNetUserId);

            var perdoruesi = await _context.Perdoruesit
                .Include(p => p.TeDhenatPerdoruesit)
                .FirstOrDefaultAsync(x => x.AspNetUserId.Equals(AspNetUserId));

            var roletEPerdoruesit = await _userManager.GetRolesAsync(user);
            var teGjithaRolet = await _roleManager.Roles.ToListAsync();

            List<string> roletPerEditimit = new List<string>();

            if (llojiEditmit.Equals("shto"))
            {
                if (roletEPerdoruesit.Count == 1 && roletEPerdoruesit.Contains("User"))
                {
                    roletPerEditimit.Add("Student");
                }

                foreach (var rolet in teGjithaRolet)
                {
                    if (!roletEPerdoruesit.Contains(rolet.Name) && !roletPerEditimit.Contains(rolet.Name) && rolet.Name != "User" && rolet.Name != "Student")
                    {
                        roletPerEditimit.Add(rolet.Name);
                    }

                }
            }

            if (llojiEditmit.Equals("fshi"))
            {
                foreach (var role in roletEPerdoruesit)
                {
                    if (!role.Equals("User") && !role.Equals("Student"))
                    {
                        roletPerEditimit.Add(role.ToString());
                    }
                }
            }

            var perdoruesiJson = new PerdoruesiJsonKthimi
            {
                UserID = perdoruesi.UserID,
                Emri = perdoruesi.Emri,
                Mbiemri = perdoruesi.Mbiemri,
                Email = perdoruesi.Email,
                Username = perdoruesi.Username,
                AspNetUserId = perdoruesi.AspNetUserId,
                TeDhenatID = perdoruesi.TeDhenatPerdoruesit.TeDhenatID,
                NrKontaktit = perdoruesi.TeDhenatPerdoruesit.NrKontaktit,
                Qyteti = perdoruesi.TeDhenatPerdoruesit.Qyteti,
                ZipKodi = perdoruesi.TeDhenatPerdoruesit.ZipKodi,
                Adresa = perdoruesi.TeDhenatPerdoruesit.Adresa,
                Shteti = perdoruesi.TeDhenatPerdoruesit.Shteti,
                Rolet = roletEPerdoruesit.ToList(),
                RoletPerEditimit = roletPerEditimit.ToList()
            };


            return Ok(perdoruesiJson);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("shfaqLlogaritERejaTeKrijuara")]
        public async Task<IActionResult> shfaqLlogaritERejaTeKrijuara()
        {
            var perdoruesit = await _context.LlogaritERejaTeKrijuara
                .ToListAsync();

            var perdoruesiList = new List<PerdoruesiJsonKthimi>();

            foreach (var perdoruesi in perdoruesit)
            {
                var user = await _userManager.FindByIdAsync(perdoruesi.AspNetUserID);
                var roletEPerdoruesit = await _userManager.GetRolesAsync(user);

                var personiNgaKerkimi = await _context.Perdoruesit.Include(x => x.TeDhenatPerdoruesit).Where(x => x.AspNetUserId == perdoruesi.AspNetUserID).FirstOrDefaultAsync();

                var perdoruesiJson = new PerdoruesiJsonKthimi
                {
                    UserID = personiNgaKerkimi.UserID,
                    Emri = personiNgaKerkimi.Emri,
                    Mbiemri = personiNgaKerkimi.Mbiemri,
                    Email = personiNgaKerkimi.Email,
                    Username = personiNgaKerkimi.Username,
                    AspNetUserId = personiNgaKerkimi.AspNetUserId,
                    TeDhenatID = personiNgaKerkimi.TeDhenatPerdoruesit.TeDhenatID,
                    NrKontaktit = personiNgaKerkimi.TeDhenatPerdoruesit.NrKontaktit,
                    Qyteti = personiNgaKerkimi.TeDhenatPerdoruesit.Qyteti,
                    ZipKodi = personiNgaKerkimi.TeDhenatPerdoruesit.ZipKodi,
                    Adresa = personiNgaKerkimi.TeDhenatPerdoruesit.Adresa,
                    Shteti = personiNgaKerkimi.TeDhenatPerdoruesit.Shteti,
                    Rolet = roletEPerdoruesit.ToList()
                };

                perdoruesiList.Add(perdoruesiJson);
            }

            return Ok(perdoruesiList);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("shfaqSipasID")]
        public async Task<IActionResult> GetbyId(string idUserAspNet)
        {
            var user = await _userManager.FindByIdAsync(idUserAspNet);

            var perdoruesi = await _context.Perdoruesit
                .Include(p => p.TeDhenatPerdoruesit)
                .FirstOrDefaultAsync(x => x.AspNetUserId.Equals(idUserAspNet));

            var roletEPerdoruesit = await _userManager.GetRolesAsync(user);


            var perdoruesiJson = new PerdoruesiJsonKthimi
            {
                UserID = perdoruesi.UserID,
                Emri = perdoruesi.Emri,
                Mbiemri = perdoruesi.Mbiemri,
                Email = perdoruesi.Email,
                Username = perdoruesi.Username,
                AspNetUserId = perdoruesi.AspNetUserId,
                TeDhenatID = perdoruesi.TeDhenatPerdoruesit.TeDhenatID,
                NrKontaktit = perdoruesi.TeDhenatPerdoruesit.NrKontaktit,
                Qyteti = perdoruesi.TeDhenatPerdoruesit.Qyteti,
                ZipKodi = perdoruesi.TeDhenatPerdoruesit.ZipKodi,
                Adresa = perdoruesi.TeDhenatPerdoruesit.Adresa,
                Shteti = perdoruesi.TeDhenatPerdoruesit.Shteti,
                Rolet = roletEPerdoruesit.ToList()
            };

            return Ok(perdoruesiJson);
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("KontrolloEmail")]
        public async Task<IActionResult> KontrolloEmail(string email)
        {
            var perdoruesi = await _userManager.FindByEmailAsync(email);

            var emailIPerdorur = false;

            if (perdoruesi != null)
            {
                emailIPerdorur = true;
            }


            return Ok(emailIPerdorur);
        }

        [Authorize(Roles = "Admin, Menaxher, User")]
        [HttpGet]
        [Route("KontrolloFjalekalimin")]
        public async Task<IActionResult> KontrolloFjalekalimin(string AspNetID, string fjalekalimi)
        {
            var perdoruesi = await _userManager.FindByIdAsync(AspNetID);

            var kontrolloFjalekalimin = await _userManager.CheckPasswordAsync(perdoruesi, fjalekalimi);

            return Ok(kontrolloFjalekalimin);
        }

        [Authorize(Roles = "Admin, Menaxher, User")]
        [HttpPost]
        [Route("NdryshoFjalekalimin")]
        public async Task<IActionResult> NdryshoFjalekalimin(string AspNetID, string fjalekalimiAktual, string fjalekalimiIRi)
        {
            var perdoruesi = await _userManager.FindByIdAsync(AspNetID);

            if (perdoruesi == null)
            {
                return BadRequest("Perdoreusi nuk egziston");
            }

            var passwodiINdryshuar = await _userManager.ChangePasswordAsync(perdoruesi, fjalekalimiAktual, fjalekalimiIRi);

            if (!passwodiINdryshuar.Succeeded)
            {
                return BadRequest("Ndodhi nje gabim gjate perditesimit te fjalekalimit");
            }

            return Ok(passwodiINdryshuar);
        }

        public class PerdoruesiJsonKthimi
        {
            public int? UserID { get; set; }
            public string? Emri { get; set; }
            public string? Mbiemri { get; set; }
            public string? Email { get; set; }
            public string? Username { get; set; }
            public string? AspNetUserId { get; set; }
            public int? TeDhenatID { get; set; }
            public string? NrKontaktit { get; set; }
            public string? Qyteti { get; set; }
            public int? ZipKodi { get; set; }
            public string? Adresa { get; set; }
            public string? Shteti { get; set; }
            public List<string>? Rolet { get; set; }
            public List<string>? RoletPerEditimit { get; set; }
        }
    }
}
