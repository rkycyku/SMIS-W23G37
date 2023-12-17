using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using W23G37.Auth;
using W23G37.Data;

namespace W23G37.Controllers
{
    [Authorize(Policy = "punonAdministrat")]
    public class RoletController : Controller
    {
        private readonly SignInManager<IdentityUser> SignInManager;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public RoletController(SignInManager<IdentityUser> signInManager, ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            SignInManager = signInManager;
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public class RoletViewModel
        {
            public string? ID { get; set; }
            public string? Name { get; set; }
            public string? NormalizedName { get; set; }
            public string? ConcurrencyStamp { get; set; }
            public int? TotaliPerdoruesve { get; set; }
        }

        // GET: RoletController
        public async Task<IActionResult> Index()
        {
            var rolet = await _roleManager.Roles.ToListAsync();

            var roletWithUsersCount = new List<RoletViewModel>();

            foreach (var roli in rolet)
            {
                var usersCount = await _userManager.GetUsersInRoleAsync(roli.Name);

                var roliWithUsersCount = new RoletViewModel()
                {
                    ID = roli.Id,
                    Name = roli.Name,
                    NormalizedName =  roli.NormalizedName,
                    ConcurrencyStamp = roli.ConcurrencyStamp,
                    TotaliPerdoruesve = usersCount.Count
                };

                roletWithUsersCount.Add(roliWithUsersCount);
            }

            return View(roletWithUsersCount);
        }

        // GET: RoletController/Details/5
        public async Task<IActionResult> Details(int id)
        {

            return View();
        }

        // GET: RoletController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: RoletController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoletViewModel roli)
        {
            if (string.IsNullOrWhiteSpace(roli.Name) || !Regex.IsMatch(roli.Name, @"^[a-zA-Z\s]+$"))
            {
                ModelState.AddModelError(nameof(roli.Name), "Roli nuk duhet te jete i zbrazet dhe duhet te permbaje vetem shkronja!");
            }

            if (ModelState.IsValid)
            {
                var ekziston = await _roleManager.FindByNameAsync(roli.Name);

                if (ekziston != null)
                {
                    return BadRequest(new AuthResults
                    {
                        Errors = new List<string> { "Ky role tashme Egziston!" }
                    });
                }
                else
                {
                    var role = new IdentityRole(roli.Name);
                    var result = await _roleManager.CreateAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return BadRequest(new AuthResults
                        {
                            Errors = new List<string> { "Ndodhi nje gabim gjate shtimit te rolit" }
                        });
                    }
                }
            }
                return View();
        }

        // GET: RoletController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View();
        }

        // POST: RoletController/Edit/5
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

        // GET: RoletController/Delete/5
        public async Task<IActionResult> Delete(string emriRolit, string idRoli)
        {
            var rolet = await _roleManager.FindByNameAsync(emriRolit); ;

            var usersCount = await _userManager.GetUsersInRoleAsync(rolet.Name);

            var roliWithUsersCount = new RoletViewModel()
            {
                ID = rolet.Id,
                Name = rolet.Name,
                NormalizedName = rolet.NormalizedName,
                ConcurrencyStamp = rolet.ConcurrencyStamp,
                TotaliPerdoruesve = usersCount.Count
            };



            return View(roliWithUsersCount);
        }

        // POST: RoletController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string name)
        {
            try
            {
                var roliEkziston = await _roleManager.FindByNameAsync(name);

                if (roliEkziston != null)
                {
                    var roliUFshi = await _roleManager.DeleteAsync(roliEkziston);

                    if (roliUFshi.Succeeded)
                    {

                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return BadRequest(new AuthResults
                        {
                            Errors = new List<string> { "Ndodhi nje gabim gjate fshirjes" }
                        });
                    }
                }
                else
                {
                    return BadRequest(new AuthResults
                    {
                        Errors = new List<string> { "Ky Rol nuk egziston" }
                    });
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
