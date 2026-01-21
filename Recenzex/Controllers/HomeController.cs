using Microsoft.AspNetCore.Mvc;
using Recenzex.Data;
using Recenzex.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;


namespace Recenzex.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var films = await _context.Films
                .OrderByDescending(f => f.Id)
                .Take(9)
                .ToListAsync();

            return View(films);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
