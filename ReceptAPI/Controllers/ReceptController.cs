using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReceptAPI.Models;

namespace ReceptAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceptController : ControllerBase
    {
        private readonly ReceptdbContext _context;

        public ReceptController(ReceptdbContext context)
        {
            _context = context;
        }

        // 8. Recept lekérdezése ID alapján
        [HttpGet("ById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var recept = _context.Recepts
                    .Include(r => r.Nehezseg)
                    .Include(r => r.Szakacs)
                    .FirstOrDefault(r => r.Id == id);

                if (recept == null)
                    return NotFound($"Nincs recept a megadott ID-val: {id}");

                var dto = new ReceptDTO
                {
                    Nev = recept.Nev,
                    ElkeszitesiIdo = recept.ElkeszitesiIdo,
                    NehezsegiSzint = recept.Nehezseg?.Szint,
                    SzakacsNev = recept.Szakacs?.Nev
                };

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest("Hiba történt: " + ex.Message);
            }
        }
    }
}
