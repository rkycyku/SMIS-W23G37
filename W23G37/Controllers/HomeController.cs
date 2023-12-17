using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using W23G37.Models;
using Microsoft.AspNetCore.Authorization;
using W23G37.Data;
using Microsoft.EntityFrameworkCore;

namespace W23G37.Controllers
{
    [Authorize(Policy = "punonAdministrat")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<IdentityUser> SignInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            SignInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }
        public class LlogariteViewModel
        {
            public Perdoruesi? Perdoruesi { get; set; }
            public TeDhenatPerdoruesit? TeDhenatPerdoruesit { get; set; }
            public List<string>? Rolet { get; set; }
        }

        public async Task<IActionResult> Index()
        {
            if (SignInManager.IsSignedIn(User))
            {
                var userId = _userManager.GetUserId(User);
                Console.WriteLine("1" + userId.ToString());

                try
                {
                    var user = await _userManager.FindByIdAsync(userId);

                    var perdoruesi = await _context.Perdoruesit
                            .Include(p => p.TeDhenatPerdoruesit)
                            .FirstOrDefaultAsync(x => x.AspNetUserId.Equals(userId));

                    var roletEPerdoruesit = await _userManager.GetRolesAsync(user);

                    var perdoruesiJson = new LlogariteViewModel
                    {
                        Perdoruesi = perdoruesi,
                        TeDhenatPerdoruesit = perdoruesi.TeDhenatPerdoruesit,
                        Rolet = roletEPerdoruesit.ToList()
                    };

                    return View(perdoruesiJson);
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Error calling API: {ex.Message}");
                    return View();
                }
            }
            else
            {
                return LocalRedirect("/Identity/Account/Login");
            }
        }



        public async Task<IActionResult> Privacy()
        {
            string savedToken = HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(savedToken))
            {
                await SignInManager.SignOutAsync();

                return LocalRedirect("/Identity/Account/Login");
            }
            else
            {
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}