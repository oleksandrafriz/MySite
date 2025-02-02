using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

public class AccountController : Controller
{
    private readonly UserService _userService;

    public AccountController(UserService userService)
    {
        _userService = userService;
    }

    public IActionResult Login() => View();

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        var user = _userService.Authenticate(username, password);
        if (user != null)
        {
            HttpContext.Session.SetString("User", user.username);
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Невірні дані!";
        return View();
    }

    public IActionResult Register() => View();

    [HttpPost]
    public IActionResult Register(string username, string email, string password)
    {
        if (string.IsNullOrEmpty(email))
        {
            ViewBag.Error = "Email обов'язковий!";
            return View();
        }

        _userService.Register(username, email, password);
        return RedirectToAction("Login");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("User");
        return RedirectToAction("Login");
    }
}
