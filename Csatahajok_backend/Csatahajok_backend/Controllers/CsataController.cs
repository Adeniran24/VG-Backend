using Csatahajok_backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Csatahajok_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CsataController : ControllerBase
{
    private readonly CsatahajokContext _context;

    public CsataController(CsatahajokContext context)
    {
        _context = context;
    }

    [HttpGet("Resztvevok/{name}")]
    public async Task<IActionResult> Resztvevok(string name)
    {
        try
        {
            var hajok = await _context.Kimenetek
                .Where(k => k.Csata == name)
                .Select(k => k.Hajo)
                .Distinct()
                .OrderBy(h => h)
                .ToListAsync();

            if (hajok.Count == 0)
            {
                return NoContent();
            }

            return Ok(hajok);
        }
        catch (Exception ex)
        {
            return BadRequest($"Hiba tortent: {ex.Message}");
        }
    }
}
