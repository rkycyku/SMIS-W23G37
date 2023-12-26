// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using W23G37.Data;
using W23G37.Models;
using static W23G37.Controllers.RoletController;

namespace W23G37.Areas.Identity.Pages.Account
{
    [Authorize(Policy = "punonAdministrat")]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Required(ErrorMessage = "Ju lutem shenoni Emrin!")]
            [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Emri duhet te permbaje vetem shkronja.")]
            public string? Emri { get; set; }

            [Required(ErrorMessage = "Ju lutem shenoni Mbiemrin!")]
            [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Mbiemri duhet te permbaje vetem shkronja.")]
            public string? Mbiemri { get; set; }

            [Required(ErrorMessage = "Ju lutem shenoni Adresen!")]
            public string? Adresa { get; set; }

            [Required(ErrorMessage = "Ju lutem shenoni Qytetin!")]
            [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Qyteti duhet te permbaje vetem shkronja.")]
            public string? Qyteti { get; set; }

            [Required(ErrorMessage = "Ju lutem shenoni Zip Kodin!")]
            [Range(1000, 99999, ErrorMessage = "Zip Kodi duhet te jete mes 1000 dhe 99999!")]
            public int? ZipKodi { get; set; }

            [Required(ErrorMessage = "Ju lutem shenoni Numri e Telefonit!")]
            [RegularExpression(@"^(?:\+\d{11}|\d{9})$", ErrorMessage = "Numri telefonit duhet te jete ne formatin: 045123123 ose +38343123132!")]
            public string? NrTelefonit { get; set; }

            [Required(ErrorMessage = "Ju lutem shenoni Daten e Lindjes!")]
            public DateTime? DataLindjes { get; set; }

            [Required(ErrorMessage = "Ju lutem shenoni Emailin Perosnal!")]
            [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Numri i telefonit duhet te jete ne formatin 38343111222, 3834411122 etj!")]
            public string? EmailPersonal { get; set; }

            [Required(ErrorMessage = "Ju lutem shenoni Emrin e Prindit!")]
            [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Emri Prindit duhet te permbaje vetem shkronja.")]
            public string? EmriPrindit { get; set; }

            [Required(ErrorMessage = "Ju lutem shenoni Numrin Personal!")]
            [RegularExpression(@"^(?:\d{10}|[A-Za-z]\d{8}[A-Za-z]|\d{13})$", ErrorMessage = "Numri Personal duhet te jete ne formatin NNNNNNNNNN! N - Numer, 10 Karaktere, LNNNNNNNNL! N - Numer & L - Shkronje, 10 Karaktere, NNNNNNNNNNNNN! N - Numer, 13 Karaktere")]
            public string? NrPersonal { get; set; }

            public string? ShtetiZgjedhur { get; set; }

            public string? GjiniaZgjedhur { get; set; }

            public string? RoliIZgjedhur { get; set; }

            public DateTime? DataKrijimit { get; set; } = DateTime.Now;
        }


        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            var rolet = await _roleManager.Roles.Where(x => x.Name != "User").Where(x => x.Name != "Student").ToListAsync();

            var roletWithUsersCount = new List<RoletViewModel>();

            foreach (var roli in rolet)
            {
                var usersCount = await _userManager.GetUsersInRoleAsync(roli.Name);

                var roliWithUsersCount = new RoletViewModel
                {
                    ID = roli.Id,
                    Name = roli.Name,
                    NormalizedName = roli.NormalizedName,
                    ConcurrencyStamp = roli.ConcurrencyStamp,
                    TotaliPerdoruesve = usersCount.Count
                };

                roletWithUsersCount.Add(roliWithUsersCount);
            }

            ViewData["Roles"] = roletWithUsersCount;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            

            if (ModelState.IsValid)
            {
                var user = CreateUser();

                var emri = Input.Emri.ToLower();
                var mbiemri = Input.Mbiemri.ToLower();

                var UsernameGjeneruar = emri.ToString() + "." + mbiemri.ToString();
                var EmailGjeneruar = UsernameGjeneruar.ToString() + "@ubt-uni.net";

                var ekziston = await _context.Perdoruesit.Where(x=> x.Email == EmailGjeneruar).ToListAsync();

                if(ekziston.Count > 0)
                {
                    UsernameGjeneruar = emri.ToString() + "." + mbiemri.ToString() + "." + (ekziston.Count + 1).ToString();
                    EmailGjeneruar = UsernameGjeneruar.ToString() + "@ubt-uni.net";
                }

                await _userStore.SetUserNameAsync(user, EmailGjeneruar.ToString(), CancellationToken.None);
                await _emailStore.SetEmailAsync(user, EmailGjeneruar.ToString(), CancellationToken.None);

                var passwordi = emri.ToString() + mbiemri.ToString() + "1@";

                var result = await _userManager.CreateAsync(user, passwordi);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);

                    await _userManager.AddToRolesAsync(user, new[] { "User", Input.RoliIZgjedhur });

                    Perdoruesi perdoruesi = new Perdoruesi()
                    {
                        AspNetUserId = userId,
                        Emri = Input.Emri,
                        Username = UsernameGjeneruar,
                        Email = EmailGjeneruar,
                        Mbiemri = Input.Mbiemri,
                    };
                    await _context.Perdoruesit.AddAsync(perdoruesi);
                    await _context.SaveChangesAsync();

                    TeDhenatPerdoruesit teDhenatPerdoruesit = new()
                    {
                        UserID = perdoruesi.UserID,
                        Adresa = Input.Adresa,
                        Qyteti = Input.Qyteti,
                        Shteti = Input.ShtetiZgjedhur,
                        ZipKodi = Input.ZipKodi > 0 ? Input.ZipKodi : 0,
                        NrKontaktit = Input.NrTelefonit,
                        DataKrijimit = Input.DataKrijimit,
                        DataLindjes = Input.DataLindjes,
                        EmailPersonal = Input.EmailPersonal,
                        EmriPrindit = Input.EmriPrindit,
                        Gjinia = Input.GjiniaZgjedhur,
                        NrPersonal = Input.NrPersonal,
                    };
                    await _context.TeDhenatPerdoruesit.AddAsync(teDhenatPerdoruesit);
                    await _context.SaveChangesAsync();

                    LlogaritERejaTeKrijuara llogaritERejaTeKrijuara = new()
                    {
                        AspNetUserID = userId,
                        PerdoruesiID = perdoruesi.UserID
                    };
                    await _context.LlogaritERejaTeKrijuara.AddAsync(llogaritERejaTeKrijuara);
                    await _context.SaveChangesAsync();

                    TempData["Message"] = "Llogaria u krijua me Sukses!";
                }


                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return LocalRedirect("/Llogarite");
            }

            var rolet = await _roleManager.Roles.Where(x => x.Name != "User").Where(x => x.Name != "Student").ToListAsync();

            var roletWithUsersCount = new List<RoletViewModel>();

            foreach (var roli in rolet)
            {
                var usersCount = await _userManager.GetUsersInRoleAsync(roli.Name);

                RoletViewModel roliWithUsersCount = new()
                {
                    ID = roli.Id,
                    Name = roli.Name,
                    NormalizedName = roli.NormalizedName,
                    ConcurrencyStamp = roli.ConcurrencyStamp,
                    TotaliPerdoruesve = usersCount.Count
                };

                roletWithUsersCount.Add(roliWithUsersCount);
            }

            ViewData["Roles"] = roletWithUsersCount;

            return Page();
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
