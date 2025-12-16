using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using randevu_sistemi.Models;

namespace randevu_sistemi.Controllers
{
    [Authorize(Roles = "Admin,Sekreter")]
    public class PatientsController : Controller
    {
        private readonly Context _context = new Context();

        public IActionResult Index()
        {
            var list = _context.Patients.ToList();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return View(patient);
            }

            _context.Patients.Add(patient);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null) return NotFound();
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return View(patient);
            }

            _context.Patients.Update(patient);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null) return NotFound();
            return View(patient);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null) return NotFound();
            return View(patient);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null) return NotFound();

            _context.Patients.Remove(patient);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
