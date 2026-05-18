using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibrarydbContext _context;

        public BooksController(LibrarydbContext context)
        {
            _context = context;
        }

     
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            try
            {
                var books = await _context.Books
                    .Include(b => b.Author)
                    .Include(b => b.Category)
                    .ToListAsync();

                return Ok(books);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        
        [HttpPost]
        public async Task<IActionResult> AddBook([FromQuery] string uid, [FromBody] Book book)
        {
            if (uid != Program.UID)
                return Unauthorized(new { message = "Nincs jogosultsága új könyv felvételéhez!" });

            try
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();

                return StatusCode(201, new
                {
                    message = "Könyv hozzáadása sikeresen megtörtént."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
