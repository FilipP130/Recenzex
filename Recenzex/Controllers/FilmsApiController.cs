using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recenzex.Data;
using Recenzex.Models;
using Recenzex.Dtos;
using Microsoft.AspNetCore.Authorization;


namespace Recenzex.Controllers
{
    [Route("api/films")]
    [ApiController]
    public class FilmsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FilmsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /api/films
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FilmDto>>> GetFilms()
        {
            var films = await _context.Films
                .Include(f => f.Genre)
                .Select(f => new FilmDto
                {
                    Id = f.Id,
                    Title = f.Title,
                    Year = f.Year,
                    PosterUrl = f.PosterUrl,
                    Description = f.Description,
                    GenreId = f.GenreId,
                    GenreName = f.Genre != null ? f.Genre.Name : ""
                })
                .ToListAsync();

            return films;
        }

        // GET: /api/films/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<FilmDto>> GetFilm(int id)
        {
            var film = await _context.Films
                .Include(f => f.Genre)
                .Where(f => f.Id == id)
                .Select(f => new FilmDto
                {
                    Id = f.Id,
                    Title = f.Title,
                    Year = f.Year,
                    PosterUrl = f.PosterUrl,
                    Description = f.Description,
                    GenreId = f.GenreId,
                    GenreName = f.Genre != null ? f.Genre.Name : ""
                })
                .FirstOrDefaultAsync();

            if (film == null) return NotFound();
            return film;
        }


        // POST: /api/films
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Film>> PostFilm(Film film)
        {
            _context.Films.Add(film);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFilm), new { id = film.Id }, film);
        }

        // PUT: /api/films/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutFilm(int id, Film film)
        {
            if (id != film.Id)
                return BadRequest("Id w URL i w obiekcie musi być takie samo.");

            _context.Entry(film).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _context.Films.AnyAsync(f => f.Id == id);
                if (!exists) return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: /api/films/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFilm(int id)
        {
            var film = await _context.Films.FindAsync(id);
            if (film == null)
                return NotFound();

            _context.Films.Remove(film);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
