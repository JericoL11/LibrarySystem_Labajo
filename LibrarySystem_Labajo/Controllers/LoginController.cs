﻿using LibrarySystem_Labajo.Data;
using LibrarySystem_Labajo.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem_Labajo.Controllers
{
    public class LoginController : Controller
    {
        //dependenc Injection
        private readonly LibrarySystem_LabajoContext _context;

        public LoginController(LibrarySystem_LabajoContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login([Bind("Username , Password")] User user)
        {

            //database checking

            var loginUser = _context.User
                            .Where(data => data.Username == user.Username && data.Password == user.Password).FirstOrDefault();

            if (loginUser == null)
            {
                ModelState.AddModelError("", "Incorrect username or password");
                return View("Index", user);
            }
            else
            {
                return RedirectToAction("Index", "Users");
            }
        }
    }
}
