using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Recenzex.Data;
using Recenzex.Models;

[Authorize]
public class CommentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public CommentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int reviewId, string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            return RedirectToAction("Index", "Home");

        var review = await _context.Reviews
            .Include(r => r.Film)
            .FirstOrDefaultAsync(r => r.Id == reviewId);

        if (review == null) return NotFound();

        var comment = new Comment
        {
            ReviewId = reviewId,
            Content = content,
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", "Films", new { id = review.FilmId });
    }
}
