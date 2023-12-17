using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using W23G37.Data;
using W23G37.Models;

namespace W23G37.API
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentetController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StudentetController(ApplicationDbContext context)
        {
            _context = context;
        }

        public class StudentetModel
        {
        }
    }
}
