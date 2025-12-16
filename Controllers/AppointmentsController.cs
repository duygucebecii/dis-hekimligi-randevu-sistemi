using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using randevu_sistemi.Models;

namespace randevu_sistemi.Controllers
{
    [Authorize] // default: herkes login olmalı
    public class AppointmentsController : Controller
    {
        private readonly Context _context = new Context();

        // Herkes (Admin, Sekreter, Doktor) genel listeyi görebilir
        [Authorize(Roles = "Admin,Sekreter,Doktor")]
        public IActionResult Index(string? search)
        {
            var appointments = _context.Appointments
                .Include(a => a.Dentist)
                .Include(a => a.Patient)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                appointments = appointments.Where(a =>
                    a.Dentist!.FullName.Contains(search) ||
                    a.Patient!.FullName.Contains(search) ||
                    a.Note!.Contains(search));
            }

            return View(appointments
                .OrderBy(a => a.DateTime)
                .ToList());
        }

        // Sadece Doktor rolü olan kullanıcı kendi randevularını görsün
        [Authorize(Roles = "Doktor")]
        public IActionResult MyAppointments()
        {
            var email = User.Identity?.Name;

            var dentist = _context.Dentists.FirstOrDefault(d => d.Email == email);
            if (dentist == null)
            {
                // Doktor kaydı yoksa boş liste dön
                return View(new List<Appointment>());
            }

            var list = _context.Appointments
                .Include(a => a.Dentist)
                .Include(a => a.Patient)
                .Where(a => a.DentistId == dentist.Id)
                .OrderBy(a => a.DateTime)
                .ToList();

            return View(list);
        }

        // Randevu detay (herkes görebilir)
        [Authorize(Roles = "Admin,Sekreter,Doktor")]
        public IActionResult Details(int id)
        {
            var appt = _context.Appointments
                .Include(a => a.Dentist)
                .Include(a => a.Patient)
                .FirstOrDefault(a => a.Id == id);

            if (appt == null) return NotFound();
            return View(appt);
        }

        // Yeni randevu sadece Admin + Sekreter
        [Authorize(Roles = "Admin,Sekreter")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.DentistId = new SelectList(_context.Dentists, "Id", "FullName");
            ViewBag.PatientId = new SelectList(_context.Patients, "Id", "FullName");
            return View();
        }

        [Authorize(Roles = "Admin,Sekreter")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.DentistId = new SelectList(_context.Dentists, "Id", "FullName", appointment.DentistId);
                ViewBag.PatientId = new SelectList(_context.Patients, "Id", "FullName", appointment.PatientId);
                return View(appointment);
            }

            bool conflict = _context.Appointments.Any(a =>
                a.DentistId == appointment.DentistId &&
                a.DateTime == appointment.DateTime);

            if (conflict)
            {
                ModelState.AddModelError("", "Bu doktora bu tarih ve saatte başka bir randevu var.");
                ViewBag.DentistId = new SelectList(_context.Dentists, "Id", "FullName", appointment.DentistId);
                ViewBag.PatientId = new SelectList(_context.Patients, "Id", "FullName", appointment.PatientId);
                return View(appointment);
            }

            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // Randevu düzenleme (Admin + Sekreter)
        [Authorize(Roles = "Admin,Sekreter")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var appt = _context.Appointments.Find(id);
            if (appt == null) return NotFound();

            ViewBag.DentistId = new SelectList(_context.Dentists, "Id", "FullName", appt.DentistId);
            ViewBag.PatientId = new SelectList(_context.Patients, "Id", "FullName", appt.PatientId);
            return View(appt);
        }

        [Authorize(Roles = "Admin,Sekreter")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.DentistId = new SelectList(_context.Dentists, "Id", "FullName", appointment.DentistId);
                ViewBag.PatientId = new SelectList(_context.Patients, "Id", "FullName", appointment.PatientId);
                return View(appointment);
            }

            bool conflict = _context.Appointments.Any(a =>
                a.Id != appointment.Id &&
                a.DentistId == appointment.DentistId &&
                a.DateTime == appointment.DateTime);

            if (conflict)
            {
                ModelState.AddModelError("", "Bu doktora bu tarih ve saatte başka bir randevu var.");
                ViewBag.DentistId = new SelectList(_context.Dentists, "Id", "FullName", appointment.DentistId);
                ViewBag.PatientId = new SelectList(_context.Patients, "Id", "FullName", appointment.PatientId);
                return View(appointment);
            }

            _context.Appointments.Update(appointment);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // Silme (Admin + Sekreter)
        [Authorize(Roles = "Admin,Sekreter")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var appt = _context.Appointments
                .Include(a => a.Dentist)
                .Include(a => a.Patient)
                .FirstOrDefault(a => a.Id == id);

            if (appt == null) return NotFound();
            return View(appt);
        }

        [Authorize(Roles = "Admin,Sekreter")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var appt = _context.Appointments.Find(id);
            if (appt == null) return NotFound();

            _context.Appointments.Remove(appt);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
