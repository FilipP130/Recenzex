using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Recenzex.Data;
using Recenzex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Recenzex.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private bool IsOwnerOrAdmin(Recenzex.Models.Review review)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return User.IsInRole("Admin") || (!string.IsNullOrWhiteSpace(userId) && review.UserId == userId);
        }

        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ReviewsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reviews
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reviews.Include(r => r.Film);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .Include(r => r.Film)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reviews/Create
        public async Task<IActionResult> Create(int? filmId)
        {
            if (filmId.HasValue)
            {
                var film = await _context.Films
                    .AsNoTracking()
                    .FirstOrDefaultAsync(f => f.Id == filmId.Value);

                if (film == null) return NotFound();

                ViewBag.LockFilm = true;
                ViewBag.FilmTitle = film.Title;
                ViewBag.FilmYear = film.Year;
                ViewBag.FilmId = film.Id;

                return View(new Review { FilmId = film.Id });
            }

            ViewBag.LockFilm = false;
            ViewData["FilmId"] = new SelectList(_context.Films, "Id", "Title");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int filmId, [Bind("Rating,Content")] Review review)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
                return Challenge();

            if (!await _context.Films.AnyAsync(f => f.Id == filmId))
                return NotFound();

            review.FilmId = filmId;
            review.UserId = userId;

            ModelState.Remove(nameof(Review.UserId));
            ModelState.Remove(nameof(Review.FilmId));

            if (!TryValidateModel(review))
            {
                ViewBag.LockFilm = true;
                var film = await _context.Films.AsNoTracking().FirstOrDefaultAsync(f => f.Id == filmId);
                ViewBag.FilmTitle = film?.Title;
                ViewBag.FilmYear = film?.Year;
                ViewBag.FilmId = filmId;

                return View(review);
            }

            _context.Reviews.Add(review);

            var filmToUpdate = await _context.Films.FindAsync(filmId);
            if (filmToUpdate != null)
            {
                filmToUpdate.RatingSum += review.Rating;
                filmToUpdate.RatingCount += 1;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Films", new { id = filmId });
        }
        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews.FindAsync(id);
           
            if (review == null)
            {
                return NotFound();
            }
            if (!IsOwnerOrAdmin(review)) return Forbid();

            ViewData["FilmId"] = new SelectList(_context.Films, "Id", "Title", review.FilmId);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Rating,Content")] Recenzex.Models.Review input)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            if (review == null) return NotFound();

            if (!IsOwnerOrAdmin(review)) return Forbid();

            ModelState.Remove("FilmId");
            ModelState.Remove("UserId");

            if (!ModelState.IsValid)
            {
                return View(review);
            }

            if (review.Rating != input.Rating)
            {
                var film = await _context.Films.FindAsync(review.FilmId);
                if (film != null)
                {
                    film.RatingSum += (input.Rating - review.Rating);
                }
            }

            review.Rating = input.Rating;
            review.Content = input.Content;

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Films", new { id = review.FilmId });
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var review = await _context.Reviews
                .Include(r => r.Film)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }
            if (!IsOwnerOrAdmin(review)) return Forbid();

            return View(review);
        }

        // POST: Reviews/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return NotFound();

            if (!IsOwnerOrAdmin(review)) return Forbid();

            var film = await _context.Films.FindAsync(review.FilmId);
            if (film != null)
            {
                film.RatingSum -= review.Rating;
                film.RatingCount -= 1;
                if (film.RatingCount < 0) film.RatingCount = 0;
                if (film.RatingSum < 0) film.RatingSum = 0;
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Films", new { id = review.FilmId });
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }
}
