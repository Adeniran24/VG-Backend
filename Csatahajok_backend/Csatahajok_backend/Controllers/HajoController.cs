using Csatahajok_backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Csatahajok_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HajoController : ControllerBase
{
    private readonly CsatahajokContext _context;

    public HajoController(CsatahajokContext context)
    {
        _context = context;
    }

    [HttpGet("All")]
    public async Task<IActionResult> All()
    {
        try
        {
            var csatak = await _context.Csatak.OrderBy(c => c.Nev).ToListAsync();
            return Ok(csatak);
        }
        catch (Exception ex)
        {
            return BadRequest($"Hiba tortent: {ex.Message}");
        }
    }

    [HttpGet("ByName/{name}")]
    public async Task<IActionResult> ByName(string name)
    {
        try
        {
            var hajo = await _context.Hajok.FindAsync(name);
            if (hajo is null)
            {
                return NotFound("Nincs ilyen nevu hajo.");
            }

            return Ok(hajo);
        }
        catch (Exception ex)
        {
            return BadRequest($"Hiba tortent: {ex.Message}");
        }
    }
}
