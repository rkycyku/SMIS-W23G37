using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using W23G37.Models;
using W23G37.Auth;
using W23G37.Data;

namespace W23G37.Areas.Identity.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthenticateController(
            UserManager<IdentityUser> userManager,
           RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("shfaqRolin")]
        public async Task<IActionResult> ShfaqRolin(string emriRolit)
        {
            var rolet = await _roleManager.FindByNameAsync(emriRolit); ;

            var usersCount = await _userManager.GetUsersInRoleAsync(rolet.Name);

            var roliWithUsersCount = new
            {
                rolet.Id,
                rolet.Name,
                rolet.NormalizedName,
                rolet.ConcurrencyStamp,
                TotaliPerdoruesve = usersCount.Count
            };


            return Ok(roliWithUsersCount);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("shfaqRolet")]
        public async Task<IActionResult> ShfaqRolet()
        {
            var rolet = await _roleManager.Roles.ToListAsync();

            var roletWithUsersCount = new List<object>();

            foreach (var roli in rolet)
            {
                var usersCount = await _userManager.GetUsersInRoleAsync(roli.Name);

                var roliWithUsersCount = new
                {
                    roli.Id,
                    roli.Name,
                    roli.NormalizedName,
                    roli.ConcurrencyStamp,
                    TotaliPerdoruesve = usersCount.Count
                };

                roletWithUsersCount.Add(roliWithUsersCount);
            }

            return Ok(roletWithUsersCount);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("ShfaqRoletPerKrijim")]
        public async Task<IActionResult> ShfaqRoletPerKrijim()
        {
            var rolet = await _roleManager.Roles.Where(x => x.Name != "User").ToListAsync();

            var roletWithUsersCount = new List<object>();

            foreach (var roli in rolet)
            {
                var usersCount = await _userManager.GetUsersInRoleAsync(roli.Name);

                var roliWithUsersCount = new
                {
                    roli.Id,
                    roli.Name,
                    roli.NormalizedName,
                    roli.ConcurrencyStamp,
                    TotaliPerdoruesve = usersCount.Count
                };

                roletWithUsersCount.Add(roliWithUsersCount);
            }

            return Ok(roletWithUsersCount);
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpGet]
        [Route("kontrolloAksesin")]
        public async Task<IActionResult> KontrolloAksesin()
        {
            return Ok("Ka Akses");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LogInModelAPI login)
        {
            if (ModelState.IsValid)
            {

                var useri_ekziston = await _userManager.FindByEmailAsync(login.Email);

                if (useri_ekziston == null)
                {
                    return BadRequest(new AuthResults()
                    {
                        Errors = new List<string>()
                        {
                            "Inavlid Payload"
                        },
                        Result = false
                    });
                }

                var neRregull = await _userManager.CheckPasswordAsync(useri_ekziston, login.Password);

                if (!neRregull)
                {
                    return BadRequest(new AuthResults()
                    {
                        Errors = new List<string>()
                        {
                            "Invalid Credintials"
                        },
                        Result = false
                    });
                }

                var roles = await _userManager.GetRolesAsync(useri_ekziston);

                var jwtToken = GenerateJwtToken(useri_ekziston, roles);

                return Ok(new AuthResults()
                {
                    Token = jwtToken,
                    Result = true
                });
            }

            return BadRequest(new AuthResults()
            {
                Errors = new List<string>()
                {
                    "Inavlid Payload"
                }
            });
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpPost]
        [Route("shtoRolinPerdoruesit")]
        public async Task<IActionResult> PerditesoAksesin(string userID, string roli)
        {
            var user = await _userManager.FindByIdAsync(userID);

            if (user == null)
            {
                return BadRequest(new AuthResults
                {
                    Errors = new List<string> { "Perdoruesi nuk ekziston!" }
                });
            }

            var perditesoAksesin = await _userManager.AddToRoleAsync(user, roli);

            if (perditesoAksesin.Succeeded)
            {

                return Ok(new AuthResults
                {
                    Result = true
                });
            }
            else if (await _userManager.IsInRoleAsync(user, roli))
            {
                return BadRequest(new AuthResults
                {
                    Errors = new List<string> { "Ky perdorues e ka kete role!" }
                });
            }
            else
            {
                return BadRequest(new AuthResults
                {
                    Errors = new List<string> { "Ndodhi nje gabim gjate perditesimit te Aksesit" }
                });
            }
        }

        [Authorize(Policy = "eshteStaf")]
        [HttpPost]
        [Route("shtoRolin")]
        public async Task<IActionResult> ShtoRolin(string roli)
        {
            var ekziston = await _roleManager.FindByNameAsync(roli);

            if (ekziston != null)
            {
                return BadRequest(new AuthResults
                {
                    Errors = new List<string> { "Ky role tashme Egziston!" }
                });
            }
            else
            {
                var role = new IdentityRole(roli);
                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return Ok(new AuthResults
                    {
                        Result = true
                    });
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


        [Authorize(Policy = "eshteStaf")]
        [HttpDelete]
        [Route("FshijRolinUserit")]
        public async Task<IActionResult> FshijRolinUserit(string userID, string roli)
        {
            var perdoruesi = await _userManager.FindByIdAsync(userID);

            if (perdoruesi == null)
            {
                return BadRequest(new AuthResults
                {
                    Errors = new List<string> { "Ky perdorues nuk egziston" }
                });
            }
            else
            {
                var ekzistonRoli = await _roleManager.FindByNameAsync(roli);

                if (ekzistonRoli != null)
                {
                    var eKaRolin = await _userManager.IsInRoleAsync(perdoruesi, roli);

                    if (eKaRolin == true)
                    {
                        await _userManager.RemoveFromRoleAsync(perdoruesi, roli);

                        return Ok(new AuthResults
                        {
                            Result = true
                        });
                    }
                }
                else
                {
                    return BadRequest(new AuthResults
                    {
                        Errors = new List<string> { "Ky role nuk egziston" }
                    });
                }

                return BadRequest(new AuthResults
                {
                    Errors = new List<string> { "Ndodhi nje gabim!" }
                });
            }
        }

        [Authorize(Policy = "punonAdministrat")]
        [HttpDelete]
        [Route("fshijRolin")]
        public async Task<IActionResult> FshijRolet(string emriRolit)
        {
            var roliEkziston = await _roleManager.FindByNameAsync(emriRolit);

            if (roliEkziston != null)
            {
                var roliUFshi = await _roleManager.DeleteAsync(roliEkziston);

                if (roliUFshi.Succeeded)
                {
                    return Ok(new AuthResults { Result = true });
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

        private string GenerateJwtToken(IdentityUser user, IList<string> roles)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);

            // Token descriptor
            var TokenDescriptor = new SecurityTokenDescriptor()
            {

                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, value:user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
                }),

                Expires = DateTime.Now.AddDays(1).AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            foreach (var role in roles)
            {
                TokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var token = jwtTokenHandler.CreateToken(TokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

    }

}
