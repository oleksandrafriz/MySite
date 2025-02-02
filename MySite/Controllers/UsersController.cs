﻿using Microsoft.AspNetCore.Mvc;
using MySite.Models;

namespace MySite.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context) {
            _context = context;
        }

        public IActionResult Index() { 
            return View();
        }
    }
}
