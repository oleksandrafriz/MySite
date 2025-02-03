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

            var username = HttpContext.Session.GetString("User");
            var isFavorite = false;

            if (username != null)
            {
                var user = _context.Users.FirstOrDefault(u => u.username == username);
                if (user != null)
                {
                    isFavorite = _context.Favorites.Any(f => f.user_id == user.Id && f.movie_id == id);
                }
            }

            ViewBag.IsFavorite = isFavorite;

            return View(movie);
        }

        public IActionResult AddToFavorites(int movieId)
        {
            var username = HttpContext.Session.GetString("User");
            if (username == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _context.Users.FirstOrDefault(u => u.username == username);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                var favorite = new Favorite
                {
                    user_id = user.Id,
                    movie_id = movieId
                };
                Console.WriteLine($"Adding Favorite: user_id={favorite.user_id}, movie_id={favorite.movie_id}");
                _context.Favorites.Add(favorite);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Error: {ex.InnerException?.Message}");
                return StatusCode(500, "Помилка при збереженні у БД.");
            }

            return RedirectToAction("Details", "Movies", new { id = movieId });
        }



        public IActionResult Favorites()
        {
            var username = HttpContext.Session.GetString("User");
            if (username == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _context.Users.FirstOrDefault(u => u.username == username);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var favoriteMovies = _context.Favorites
                .Where(f => f.user_id == user.Id)
                .Select(f => f.Movie)
                .ToList();

            return View(favoriteMovies);
        }


        public IActionResult RemoveFromFavorites(int movieId)
        {
            var username = HttpContext.Session.GetString("User");
            if (username == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = _context.Users.FirstOrDefault(u => u.username == username);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var favorite = _context.Favorites.FirstOrDefault(f => f.user_id == user.Id && f.movie_id == movieId);
            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                _context.SaveChanges();
            }

            return RedirectToAction("Favorites");
        }


    }
}
