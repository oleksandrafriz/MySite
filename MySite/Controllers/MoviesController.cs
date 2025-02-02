using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySite.Models;
namespace MySite.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var movies = _context.Movies
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre) // Завантажуємо жанри
                .ToList();

            ViewBag.Genres = _context.Genres.Select(g => g.name).Distinct().ToList();

            return View(movies);
        }

        public IActionResult Details(int id)
        {
            var movie = _context.Movies
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre) // Завантажуємо жанри
                .FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
    }
}
