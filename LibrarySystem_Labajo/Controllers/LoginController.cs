using LibrarySystem_Labajo.Data;
using LibrarySystem_Labajo.Models;
using LibrarySystem_Labajo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;

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

            //Assign Userpassword to its original state
            string harshPassword = HashingService.HashData(user.Password);
            user.Password = harshPassword;
         
            /*
             SESSION GET AND SET
            HttpContext.Session.SetString(“key”,”value”)
            HttpContext.Session.GetString(“key”)   
             */

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

                //5. Set session  (6. goto "Users View" then select  _Layout_Users.cshtml)
                HttpContext.Session.SetString("Name", $"{loginUser.FirstName} {loginUser.LastName}");

                return RedirectToAction("Index", "Users");
            }
        }
    }
}
