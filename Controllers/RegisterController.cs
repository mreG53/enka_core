using Microsoft.AspNetCore.Mvc;
using stokEnka.Models;

namespace stokEnka.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegisterController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View(new User());
        }

        [HttpPost]
        public IActionResult RegisterUser(User model)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Success");
            }
            return View(model);
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
