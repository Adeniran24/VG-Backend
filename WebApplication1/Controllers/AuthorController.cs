using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly LibrarydbContext _context;

        public AuthorsController(LibrarydbContext context)
        {
            _context = context;
        }

      
        [HttpGet("{name}")]
        public async Task<IActionResult> GetBooksByAuthor(string name)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.AuthorName == name);

            if (author == null)
                return NotFound(new { message = "A szerző nem található!" });

            return Ok(author.Books);
        }

        [HttpGet("Count")]
        public async Task<IActionResult> GetAuthorsCount()
        {
            try
            {
                var count = await _context.Authors.CountAsync();
                return Ok(new { authorsCount = count });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
