using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HospitalApp.Models;
using HospitalApp.ViewModels;
using HospitalApp;
using Microsoft.AspNetCore.Authorization;
using HospitalApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[Authorize(Roles = "Admin,User")]
public class AppointmentController : Controller
{
    private readonly StoreDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public AppointmentController(StoreDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Appointments/Create
    public IActionResult Create()
    {
        ViewBag.Clinics = _context.Clinics.ToList();
        ViewBag.Doctors = _context.Doctors.ToList();
        ViewBag.user = _userManager.GetUserId(User);
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Appointment model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Clinics = _context.Clinics.ToList();
            ViewBag.Doctors = _context.Doctors.ToList();
            ViewBag.user = _userManager.GetUserId(User);
            return View(model);
        }
        
        var userId = _userManager.GetUserId(User);
        model.PatientId = userId;

        // Get selected doctor information
        var doctor = await _context.Doctors.FindAsync(model.DoctorId);
        model.Doctor = doctor; // Add doctor information to the model

        ViewBag.DName = model.Doctor.Name;

        // Get timeslots
        var availableTimes = GetTimes();
        ViewBag.time = availableTimes;

        return View("AvailableTimes", model);
    }

    [HttpPost]
    public async Task<ActionResult> AvailableTimes(Appointment appointment)
    {
        // Fill timeslots
        ViewBag.time = GetTimes();

        // Check and fill in doctor information if it is missing
        if (appointment.DoctorId > 0)
        {
            var doctor = await _context.Doctors.FindAsync(appointment.DoctorId);
            if (doctor != null)
            {
                ViewBag.DoctorName = doctor.Name;
            }
            else
            {
                ViewBag.DoctorName = "Unknown Doctor";
            }
        }

        // Check appointments for the same date and time only for the selected doctor
        var existingAppointment = await _context.Appointments
            .Where(a =>
                a.DoctorId == appointment.DoctorId && // Only this doctor
                a.AppointmentDate.Date == appointment.AppointmentDate.Date && // Same Day
                a.AppointmentTime == appointment.AppointmentTime) // Same Hour
            .FirstOrDefaultAsync();

        if (existingAppointment != null)
        {
            // If the appointment is already booked, add an error message and show the form again.
            ViewData["ErrorMessage"] = $"Doctor {ViewBag.DoctorName} is already booked at {appointment.AppointmentTime}. Please select another time.";
            return View("AvailableTimes", appointment);
        }

        // If there is no appointment on that date and day, add it to the database
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        // If added successfully, redirect to appointment details page
        return RedirectToAction("AppointmentDetails", "Appointment");
    }

    // Time production method
    private List<TimeSpan> GetTimes()
    {
        var startTime = new TimeSpan(9, 0, 0); // Starting: 09:00
        var endTime = new TimeSpan(17, 0, 0);  // Finishing: 17:00
        var timeSlots = new List<TimeSpan>();

        for (var time = startTime; time < endTime; time += TimeSpan.FromMinutes(20))
        {
            timeSlots.Add(time);
        }
        return timeSlots;
    }

    // For fetch process with Ajax
    [HttpGet]
    public IActionResult GetDoctorsByClinic(int clinicId)
    {
        // Filters doctors in that clinic based on their clinic ID.
        var doctors = _context.Doctors
            .Where(d => d.ClinicId == clinicId)  // Doctors with matching clinic IDs
            .Select(d => new { d.Id, d.Name }) // Get ID and name information
            .ToList();

        return Json(doctors);  // Returns doctors in JSON format.
    }

    [HttpGet]
    public async Task<IActionResult> AppointmentDetails()
    {
        var userId = _userManager.GetUserId(User); // Gets the user's ID.

        if (userId == null)  // If the user is not logged in, he/she is redirected to the login page.
        {
            return RedirectToAction("Login", "Account");
        }

        var isAdmin = User.IsInRole("Admin");
        IQueryable<Appointment> appointmentsQuery;

        if (isAdmin)
        {
            appointmentsQuery = _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Clinic)
                .OrderBy(a => a.AppointmentDate);
        }
        else
        {
            // Normal users should only see their own appointments
            appointmentsQuery = _context.Appointments
                .Where(a => a.PatientId == userId)
                .Include(a => a.Doctor)
                .Include(a => a.Clinic)
                .OrderBy(a => a.AppointmentDate);
        }

        var pastAppointments = await appointmentsQuery
            .Where(a => a.AppointmentDate < DateTime.Now)
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync();

        var upcomingAppointments = await appointmentsQuery
            .Where(a => a.AppointmentDate >= DateTime.Now)
            .OrderBy(a => a.AppointmentDate)
            .ToListAsync();

        var model = new AppointmentDetailsViewModel
        {
            PastAppointments = pastAppointments,
            UpcomingAppointments = upcomingAppointments
        };

        return View(model);
    }

    // Some CRUD Operations
    public IActionResult EditClinics()
    {
        var clinics = _context.Clinics.ToList();
        return View(clinics);
    }

    public IActionResult EditDoctors()
    {
        var doctors = _context.Doctors.ToList();
        return View(doctors);
    }

    public IActionResult DeleteClinics()
    {
        var clinics = _context.Clinics.ToList();
        return View(clinics);
    }

    public IActionResult DeleteDoctors()
    {
        var doctors = _context.Doctors.ToList();
        return View(doctors);
    }

    // Admin-only view
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AdminAppointments()
    {
        // Get all appointments with details
        var appointments = await _context.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Clinic)
            .ToListAsync();

        // Separate past and upcoming appointments
        var pastAppointments = appointments
            .Where(a => a.AppointmentDate < DateTime.Now)
            .OrderByDescending(a => a.AppointmentDate)
            .ToList();

        var upcomingAppointments = appointments
            .Where(a => a.AppointmentDate >= DateTime.Now)
            .OrderBy(a => a.AppointmentDate)
            .ToList();

        var model = new AdminAppointmentViewModel
        {
            PastAppointments = pastAppointments,
            UpcomingAppointments = upcomingAppointments
        };

        return View(model); // Pass the ViewModel to the Admin view
    }
}
