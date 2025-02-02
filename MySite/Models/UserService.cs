using System.Security.Cryptography;
using System.Text;
using System.Linq;
using MySite.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

public class UserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Register(string username, string email, string password)
    {
        var user = new User
        {
            username = username,
            email = email,
            password_hash = HashPassword(password)
        };

        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public User Authenticate(string username, string password)
    {
        var user = _context.Users.SingleOrDefault(u => u.username == username);
        if (user == null || user.password_hash != HashPassword(password))
        {
            return null;
        }
        return user;
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }
}
