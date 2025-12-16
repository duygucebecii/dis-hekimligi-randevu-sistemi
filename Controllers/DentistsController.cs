using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using randevu_sistemi.Models;

namespace randevu_sistemi.Controllers
{
    [Authorize(Roles = "Admin,Sekreter")]
    public class DentistsController : Controller
    {
        private readonly Context _context = new Context();

        public IActionResult Index()
        {
            var list = _context.Dentists.ToList();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Dentist dentist)
        {
            if (!ModelState.IsValid)
            {
                return View(dentist);
            }

            _context.Dentists.Add(dentist);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var dentist = _context.Dentists.Find(id);
            if (dentist == null) return NotFound();
            return View(dentist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Dentist dentist)
        {
            if (!ModelState.IsValid)
            {
                return View(dentist);
            }

            _context.Dentists.Update(dentist);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var dentist = _context.Dentists.Find(id);
            if (dentist == null) return NotFound();
            return View(dentist);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var dentist = _context.Dentists.Find(id);
            if (dentist == null) return NotFound();
            return View(dentist);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var dentist = _context.Dentists.Find(id);
            if (dentist == null) return NotFound();

            _context.Dentists.Remove(dentist);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
