using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using stokEnka.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace stokEnka.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public RegisterController(ApplicationDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View(new User());
        }

        //Register User
        [HttpPost]
        public async Task<IActionResult> RegisterUser(User model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.FirstOrDefault(u => u.Email == model.Email || u.Phone == model.Phone);
                if (existingUser != null)
                {
                    if (existingUser.Email == model.Email)
                        ModelState.AddModelError("Email", "Bu e-posta adresi zaten kullanılıyor.");
                    if (existingUser.Phone == model.Phone)
                        ModelState.AddModelError("Phone", "Bu telefon numarası zaten kullanılıyor.");
                    return View(model); // hataları forma tekrar gönder
                }

                if (_passwordHasher == null)
                    throw new InvalidOperationException("PH not init.");

                model.Password = _passwordHasher.HashPassword(model, model.Password);

                _context.Users.Add(model);
                await _context.SaveChangesAsync();
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