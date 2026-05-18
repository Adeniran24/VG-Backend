using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReceptAPI.Models;

namespace ReceptAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HozzavaloController : ControllerBase
    {
        private readonly ReceptdbContext _context;

        public HozzavaloController(ReceptdbContext context)
        {
            _context = context;
        }

        // 7. Összes hozzávaló
        [HttpGet("All")]
        public IActionResult GetAll()
        {
            try
            {
                var list = _context.Hozzavalos.ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest("Hiba az adatok lekérése közben. " + ex.Message);
            }
        }

        // 9. Új hozzávaló
        [HttpPost("Uj")]
        public async Task<IActionResult> Uj([FromBody] Hozzavalo hozzavalo)
        {
            try
            {
                await _context.Hozzavalos.AddAsync(hozzavalo);
                await _context.SaveChangesAsync();
                return StatusCode(201, "Sikeres mentés");
            }
            catch (Exception ex)
            {
                return BadRequest("Hiba mentés közben. " + ex.Message);
            }
        }
    }
}
