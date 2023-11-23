using LibrarySystem_Labajo.Data;
using Microsoft.AspNetCore.Mvc;
//Class model 
using LibrarySystem_Labajo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Cryptography.X509Certificates;

namespace LibrarySystem_Labajo.Controllers
{
    public class SignupController : Controller

    {
        //Depenc Injection
        private readonly LibrarySystem_LabajoContext _context;

        public SignupController(LibrarySystem_LabajoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            string message = TempData["Message"] as string;

            ViewBag.Message = message;

            // Set a flag to indicate the visibility of the hidden element
            ViewBag.ShowHidden = true; // Set it to true or false based on your condition
           
        
            return View();
        }

        //FOR CREATE FUNCTIONS
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("id,FirstName,LastName,Username,Password,confirm_Password,BirthDate")] User user)
        {
            if (ModelState.IsValid)
            {
             
                _context.Add(user);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Successfully registered!";

              
               /* return View();*/
                   return RedirectToAction(nameof(Index));
            }
           
            return View(user);
        }

       
        
    }
}

