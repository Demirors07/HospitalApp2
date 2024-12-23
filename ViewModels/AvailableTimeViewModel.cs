using System.ComponentModel.DataAnnotations;

namespace HospitalApp.ViewModels {
    public class AvailableTimeViewModel
{
    public long DoctorId { get; set; }
    public string? DoctorName { get; set; } // New area
    public DateTime AppointmentDate { get; set; }
 
    public string ? PatientID { get; set; } // New area
}

}
