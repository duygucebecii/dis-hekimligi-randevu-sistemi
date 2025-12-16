using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using randevu_sistemi.Models;
using System.Diagnostics;

namespace randevu_sistemi.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly Context _context = new Context();

        public IActionResult Index()
        {
            var upcoming = _context.Appointments
                .OrderBy(a => a.DateTime)
                .Where(a => a.DateTime >= DateTime.Now)
                .Take(5)
                .ToList();

            ViewBag.DentistCount = _context.Dentists.Count();
            ViewBag.PatientCount = _context.Patients.Count();
            ViewBag.AppointmentCount = _context.Appointments.Count();

            return View(upcoming);
        }
    }
}
