using HospitalApp.Models;
namespace HospitalApp.ViewModels
{
    public class AdminAppointmentViewModel
    {
        public List<Appointment> PastAppointments { get; set; } = new List<Appointment>();
        public List<Appointment> UpcomingAppointments { get; set; } = new List<Appointment>();
    
    }
}
