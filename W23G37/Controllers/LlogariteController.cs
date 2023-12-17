using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using W23G37.Data;
using W23G37.Models;

namespace W23G37.Controllers
{
    [Authorize(Policy = "punonAdministrat")]
    public class LlogariteController : Controller
    {
        private readonly SignInManager<IdentityUser> SignInManager;
        private readonly UserManager<IdentityUser> UserManager;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public LlogariteController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            SignInManager = signInManager;
            UserManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }

        public class LlogariteViewModel
        {
            public Perdoruesi? Perdoruesi { get; set; }
            public List<Perdoruesi>? Perdoruesit { get; set; }
            public TeDhenatPerdoruesit? TeDhenatPerdoruesit { get; set; }
            public List<TeDhenatPerdoruesit>? TeDhenatPerdoruesve { get; set; }
            public List<string>? Rolet { get; set; }
            public string? roliZgjedhur { get; set; }
            public List<string>? RoletPerEditimit { get; set; }
        }

        // GET: LlogariteController
        public async Task<IActionResult> Index()
        {
            var perdoruesit = await _context.LlogaritERejaTeKrijuara
                 .ToListAsync();

            var perdoruesiList = new List<LlogariteViewModel>();

            foreach (var perdoruesi in perdoruesit)
            {
                var user = await UserManager.FindByIdAsync(perdoruesi.AspNetUserID);
                var roletEPerdoruesit = (await UserManager.GetRolesAsync(user)).Where(role => role != "User").ToList(); ;

                var personiNgaKerkimi = await _context.Perdoruesit.Include(x => x.TeDhenatPerdoruesit).Where(x => x.AspNetUserId == perdoruesi.AspNetUserID).FirstOrDefaultAsync();

                var perdoruesiJson = new LlogariteViewModel
                {
                    Perdoruesi = personiNgaKerkimi,
                    TeDhenatPerdoruesit = personiNgaKerkimi?.TeDhenatPerdoruesit,
                    Rolet = roletEPerdoruesit.ToList()
                };

                perdoruesiList.Add(perdoruesiJson);
            }

            return View(perdoruesiList);
        }

        public async Task<IActionResult> RoletPerUser()
        {
            var perdoruesit = await _context.Perdoruesit.Include(x => x.TeDhenatPerdoruesit)
                .ToListAsync();

            var perdoruesiList = new List<LlogariteViewModel>();

            foreach (var perdoruesi in perdoruesit)
            {
                var user = await UserManager.FindByIdAsync(perdoruesi.AspNetUserId);
                var roles = (await UserManager.GetRolesAsync(user)).Where(role => role != "User").ToList();

                var eshteStudent = await UserManager.IsInRoleAsync(user, "Student");

                if (eshteStudent == false)
                {

                    var personiNgaKerkimi = await _context.Perdoruesit.Include(x => x.TeDhenatPerdoruesit).Where(x => x.AspNetUserId == perdoruesi.AspNetUserId).FirstOrDefaultAsync();


                    var perdoruesiJson = new LlogariteViewModel
                    {
                        Perdoruesi = personiNgaKerkimi,
                        TeDhenatPerdoruesit = personiNgaKerkimi?.TeDhenatPerdoruesit,
                        Rolet = roles.ToList()
                    };

                    perdoruesiList.Add(perdoruesiJson);
                }
            }

            return View(perdoruesiList);
        }

        // GET: LlogariteController/Edit/5
        public async Task<IActionResult> ShtoRoletUser(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            var perdoruesi = await _context.Perdoruesit
                .Include(p => p.TeDhenatPerdoruesit)
                .FirstOrDefaultAsync(x => x.AspNetUserId.Equals(id));

            var roletEPerdoruesit = await UserManager.GetRolesAsync(user);
            var teGjithaRolet = await _roleManager.Roles.ToListAsync();

            List<string> roletPerEditimit = new List<string>();

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


            var perdoruesiJson = new LlogariteViewModel
            {
                Perdoruesi = perdoruesi,
                TeDhenatPerdoruesit = perdoruesi?.TeDhenatPerdoruesit,
                Rolet = roletEPerdoruesit.ToList(),
                RoletPerEditimit = roletPerEditimit.ToList()
            };


            return View(perdoruesiJson);
        }

        // POST: LlogariteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShtoRoletUser(string AspNetUserId, string roliZgjedhur)
        {
            try
            {
                var user = await UserManager.FindByIdAsync(AspNetUserId);

                await UserManager.AddToRoleAsync(user, roliZgjedhur);

                return RedirectToAction(nameof(RoletPerUser));
            }
            catch
            {
                return View();
            }
        }

        // GET: LlogariteController/Edit/5
        public async Task<IActionResult> FshijRoletUser(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            var perdoruesi = await _context.Perdoruesit
                .Include(p => p.TeDhenatPerdoruesit)
                .FirstOrDefaultAsync(x => x.AspNetUserId.Equals(id));

            var roletEPerdoruesit = await UserManager.GetRolesAsync(user);
            var teGjithaRolet = await _roleManager.Roles.ToListAsync();

            List<string> roletPerEditimit = new List<string>();

            foreach (var role in roletEPerdoruesit)
            {
                if (!role.Equals("User") && !role.Equals("Student"))
                {
                    roletPerEditimit.Add(role.ToString());
                }
            }


            var perdoruesiJson = new LlogariteViewModel
            {
                Perdoruesi = perdoruesi,
                TeDhenatPerdoruesit = perdoruesi?.TeDhenatPerdoruesit,
                Rolet = roletEPerdoruesit.ToList(),
                RoletPerEditimit = roletPerEditimit.ToList()
            };


            return View(perdoruesiJson);
        }

        // POST: LlogariteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FshijRoletUser(string AspNetUserId, string roliZgjedhur)
        {
            try
            {
                var user = await UserManager.FindByIdAsync(AspNetUserId);

                await UserManager.RemoveFromRoleAsync(user, roliZgjedhur);

                return RedirectToAction(nameof(RoletPerUser));
            }
            catch
            {
                return View();
            }
        }

        // GET: LlogariteController/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            var perdoruesi = await _context.Perdoruesit
                .Include(p => p.TeDhenatPerdoruesit)
                .FirstOrDefaultAsync(x => x.AspNetUserId.Equals(id));

            var roletEPerdoruesit = (await UserManager.GetRolesAsync(user)).Where(role => role != "User").ToList(); 


            var perdoruesiJson = new LlogariteViewModel()
            {
                Perdoruesi = perdoruesi,
                TeDhenatPerdoruesit = perdoruesi?.TeDhenatPerdoruesit,
                Rolet = roletEPerdoruesit.ToList()
            };

            return View(perdoruesiJson);
        }

        // GET: LlogariteController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: LlogariteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LlogariteController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View();
        }

        // POST: LlogariteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LlogariteController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View();
        }

        // POST: LlogariteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
