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

            [Required(ErrorMessage = "Ju lutem shenoni Shtetin!")]
            [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Shteti duhet te permbaje vetem shkronja!")]
            public string? Shteti { get; set; }

            [Required(ErrorMessage = "Ju lutem shenoni Zip Kodin!")]
            [Range(10000, 99999, ErrorMessage = "Zip Kodi duhet te jete mes 10000 dhe 99999!")]
            public int? ZipKodi { get; set; }

            [Required(ErrorMessage = "Ju lutem shenoni Numri e Telefonit!")]
            [RegularExpression(@"^3834[345689][0-9]{6}$", ErrorMessage = "Numri i telefonit duhet te jete ne formatin 38343111222, 3834411122 etj!")]
            public string? NrTelefonit { get; set; }

            public string? RoliIZgjedhur { get; set; }
        }


        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            var rolet = await _roleManager.Roles.Where(x => x.Name != "User").ToListAsync();

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

                var totaliPerdoruesve = await _userManager.Users.CountAsync();
                var numriRendorPerdoruesit = totaliPerdoruesve + 1;

                var emri = Input.RoliIZgjedhur != "Student" ? Input.Emri.ToLower() : Input.Emri.Trim().ToLower();
                var mbiemri = Input.RoliIZgjedhur != "Student" ? Input.Mbiemri.ToLower() : Input.Mbiemri.Trim().ToLower();

                var emriShkronjaPare = emri[0];
                var mbiemriShkronjaPare = mbiemri[0];

                var UsernameGjeneruar = Input.RoliIZgjedhur != "Student" ? emri.ToString() + "." + mbiemri.ToString() : emriShkronjaPare.ToString() + mbiemriShkronjaPare.ToString() + numriRendorPerdoruesit.ToString();
                var EmailGjeneruar = UsernameGjeneruar.ToString() + "@ubt-uni.net";

                var ekziston = await _context.Perdoruesit.Where(x=> x.Email == EmailGjeneruar).ToListAsync();

                if(ekziston.Count > 0)
                {
                    UsernameGjeneruar = emri.ToString() + "." + mbiemri.ToString() + "." + (ekziston.Count + 1).ToString();
                    EmailGjeneruar = UsernameGjeneruar.ToString() + "@ubt-uni.net";
                }

                await _userStore.SetUserNameAsync(user, EmailGjeneruar.ToString(), CancellationToken.None);
                await _emailStore.SetEmailAsync(user, EmailGjeneruar.ToString(), CancellationToken.None);

                var passwordi = Input.RoliIZgjedhur != "Student" ? emri.ToString() + mbiemri.ToString() + "1@" : EmailGjeneruar; // Set the password you want to use

                var result = await _userManager.CreateAsync(user, passwordi);



                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);

                    await _userManager.AddToRolesAsync(user, new[] { "User", Input.RoliIZgjedhur });

                    Perdoruesi perdoruesi = new()
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
                        Shteti = Input.Shteti,
                        ZipKodi = Input.ZipKodi > 0 ? Input.ZipKodi : 0,
                        NrKontaktit = Input.NrTelefonit
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

                }



                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return LocalRedirect("/Llogarite");

            }

            var rolet = await _roleManager.Roles.Where(x => x.Name != "User").ToListAsync();

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
