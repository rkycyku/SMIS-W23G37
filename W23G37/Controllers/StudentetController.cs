using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using W23G37.Data;
using W23G37.Models;

namespace W23G37.Controllers
{
    public class StudentetController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public StudentetController(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _context = context;
        }

        public class StudentetViewModel
        {
            public virtual AplikimetEReja? AplikimiIRi { get; set; }
            public virtual List<AplikimetEReja>? AplikimetEReja { get; set; }
        }

        public async Task<IActionResult> AplikimetERejaIndex()
        {
            var aplikimetEReja = await _context.AplikimetEReja.Include(x => x.Departamentet).Include(x => x.NiveliStudimeve).Include(x => x.Pagesat).ToListAsync();

            StudentetViewModel modeli = new()
            {
                AplikimetEReja = aplikimetEReja,
            };

            return View(modeli);
        }

        public async Task<IActionResult> ShtoniLlogarinStudentit(int id)
        {
            var studenti = await _context.AplikimetEReja.Where(x => x.AplikimiID == id).FirstOrDefaultAsync();

            var user = CreateUser();

            var totaliPerdoruesve = await _userManager.Users.CountAsync();
            var numriRendorPerdoruesit = totaliPerdoruesve + 1;

            var emri = studenti.Emri.Trim().ToLower();
            var mbiemri = studenti.Mbiemri.Trim().ToLower();

            var emriShkronjaPare = emri[0];
            var mbiemriShkronjaPare = mbiemri[0];

            var UsernameGjeneruar = emriShkronjaPare.ToString() + mbiemriShkronjaPare.ToString() + numriRendorPerdoruesit.ToString();
            var EmailGjeneruar = UsernameGjeneruar.ToString() + "@ubt-uni.net";

            var ekziston = await _context.Perdoruesit.Where(x => x.Email == EmailGjeneruar).ToListAsync();

            if (ekziston.Count > 0)
            {
                UsernameGjeneruar = emri.ToString() + "." + mbiemri.ToString() + "." + (ekziston.Count + 1).ToString();
                EmailGjeneruar = UsernameGjeneruar.ToString() + "@ubt-uni.net";
            }

            await _userStore.SetUserNameAsync(user, EmailGjeneruar.ToString(), CancellationToken.None);
            await _emailStore.SetEmailAsync(user, EmailGjeneruar.ToString(), CancellationToken.None);

            var passwordi = EmailGjeneruar; // Set the password you want to use

            var result = await _userManager.CreateAsync(user, passwordi);

            if (result.Succeeded)
            {
                var userId = await _userManager.GetUserIdAsync(user);

                await _userManager.AddToRolesAsync(user, new[] { "User", "Student" });

                Perdoruesi perdoruesi = new Perdoruesi
                {
                    AspNetUserId = userId,
                    Emri = studenti.Emri,
                    Username = UsernameGjeneruar,
                    Email = EmailGjeneruar,
                    Mbiemri = studenti.Mbiemri,
                };
                await _context.Perdoruesit.AddAsync(perdoruesi);
                await _context.SaveChangesAsync();

                TeDhenatPerdoruesit teDhenatPerdoruesit = new()
                {
                    UserID = perdoruesi.UserID,
                    Adresa = studenti.Adresa,
                    Qyteti = studenti.Qyteti,
                    Shteti = studenti.Shteti,
                    ZipKodi = studenti.ZipKodi > 0 ? studenti.ZipKodi : 0,
                    NrKontaktit = studenti.NrKontaktit,
                    DataLindjes = studenti.DataLindjes,
                    EmailPersonal = studenti.EmailPersonal,
                    EmriPrindit = studenti.EmriPrindit,
                    Gjinia = studenti.Gjinia,
                    NrPersonal = studenti.NrPersonal,
                };
                await _context.TeDhenatPerdoruesit.AddAsync(teDhenatPerdoruesit);
                await _context.SaveChangesAsync();

                TeDhenatRegjistrimitStudentit teDhenatRegjistrimitStudentit = new()
                {
                    UserId = perdoruesi.UserID,
                    DepartamentiID = studenti.DepartamentiID,
                    DataRegjistrimit = studenti.DataRegjistrimit,
                    KodiFinanciar = studenti.KodiFinanciar,
                    LlojiRegjistrimit = studenti.LlojiRegjistrimit,
                    NiveliStudimitID = studenti.NiveliStudimitID,
                    VitiAkademikRegjistrim = studenti.VitiAkademikRegjistrim,
                };

                await _context.TeDhenatRegjistrimitStudentit.AddAsync(teDhenatRegjistrimitStudentit);
                await _context.SaveChangesAsync();

                LlogaritERejaTeKrijuara llogaritERejaTeKrijuara = new LlogaritERejaTeKrijuara
                {
                    AspNetUserID = userId,
                    PerdoruesiID = perdoruesi.UserID
                };
                await _context.LlogaritERejaTeKrijuara.AddAsync(llogaritERejaTeKrijuara);
                await _context.SaveChangesAsync();

                var pagesaERegjistrimit = await _context.Pagesat.Where(x => x.AplikimiID == id).FirstOrDefaultAsync();

                pagesaERegjistrimit.AplikimiID = null;
                pagesaERegjistrimit.PersoniID = perdoruesi.UserID;

                _context.Pagesat.Update(pagesaERegjistrimit);
                await _context.SaveChangesAsync();


                _context.AplikimetEReja.Remove(studenti);
                await _context.SaveChangesAsync();
            }

            TempData["Message"] = "Llogaria per studentin u krija me sukses!";

            return RedirectToAction("AplikimetERejaIndex");
        }

        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }
        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }
    }
}
