using Microsoft.AspNetCore.Mvc;
using ReceptAPI.Models;

namespace ReceptAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SzakacsController : ControllerBase
    {
        private readonly ReceptdbContext _context;

        public SzakacsController(ReceptdbContext context)
        {
            _context = context;
        }

        // 10. Módosítás
        [HttpPut("Modosit")]
        public async Task<IActionResult> Modosit([FromBody] Szakac szakacs)
        {
            try
            {
                var letezo = await _context.Szakacs.FindAsync(szakacs.Id);

                if (letezo == null)
                    return NotFound("Nem azonosítható szakács");

                letezo.Nev = szakacs.Nev;
                letezo.Email = szakacs.Email;
                letezo.Telefonszam = szakacs.Telefonszam;

                await _context.SaveChangesAsync();
                return Ok("Sikeres módosítás");
            }
            catch (Exception ex)
            {
                return BadRequest("Hiba módosítás közben. " + ex.Message);
            }
        }

        // 11. Törlés
        [HttpDelete("Torol/{id}")]
        public IActionResult Torol(int id)
        {
            try
            {
                var szakacs = _context.Szakacs.Find(id);

                if (szakacs == null)
                    return NotFound($"Nincs szakács a megadott ID-val: {id}");

                _context.Szakacs.Remove(szakacs);
                _context.SaveChanges();

                return Ok("Sikeres törlés");
            }
            catch (Exception ex)
            {
                return BadRequest("Hiba törlés közben. " + ex.Message);
            }
        }
    }
}
