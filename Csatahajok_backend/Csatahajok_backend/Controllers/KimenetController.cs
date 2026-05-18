using Csatahajok_backend.Data;
using Csatahajok_backend.Dtos;
using Csatahajok_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Csatahajok_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class KimenetController : ControllerBase
{
    private readonly CsatahajokContext _context;

    public KimenetController(CsatahajokContext context)
    {
        _context = context;
    }

    [HttpPost("UjKimenet")]
    public async Task<IActionResult> UjKimenet([FromBody] KimenetCreateDto dto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dto.Hajo) || string.IsNullOrWhiteSpace(dto.Csata) || string.IsNullOrWhiteSpace(dto.Eredmeny))
            {
                return BadRequest("Minden mezo kitoltese kotelezo.");
            }

            var hajoLetezik = await _context.Hajok.AnyAsync(h => h.Nev == dto.Hajo);
            var csataLetezik = await _context.Csatak.AnyAsync(c => c.Nev == dto.Csata);

            if (!hajoLetezik || !csataLetezik)
            {
                return BadRequest("A megadott hajo vagy csata nem letezik.");
            }

            var marLetezik = await _context.Kimenetek.AnyAsync(k => k.Hajo == dto.Hajo && k.Csata == dto.Csata);
            if (marLetezik)
            {
                return BadRequest("Ez a kimenet mar letezik.");
            }

            var ujKimenet = new Kimenet
            {
                Hajo = dto.Hajo,
                Csata = dto.Csata,
                Eredmeny = dto.Eredmeny
            };

            await _context.Kimenetek.AddAsync(ujKimenet);
            await _context.SaveChangesAsync();

            return Ok("Uj kimenet sikeresen rogzitve.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Hiba tortent: {ex.Message}");
        }
    }

    [HttpDelete("KimenetTorles/{csata}/{hajonev}")]
    public async Task<IActionResult> KimenetTorles(string csata, string hajonev)
    {
        try
        {
            var kimenet = await _context.Kimenetek.FindAsync(hajonev, csata);
            if (kimenet is null)
            {
                return NotFound("A torlendo kimenet nem talalhato.");
            }

            _context.Kimenetek.Remove(kimenet);
            await _context.SaveChangesAsync();

            return Ok("Kimenet torolve.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Hiba tortent: {ex.Message}");
        }
    }
}
