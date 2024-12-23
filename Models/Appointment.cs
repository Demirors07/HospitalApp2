using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApp.Models
{
    public class Appointment
    {
        [Key] // Primary Key
        public long Id { get; set; }



        [ForeignKey("Clinic")] // Foreign Key to Clinic
         [Required(ErrorMessage = "Please select a clinic.")]
        public long ClinicId { get; set; }

        public Clinic? Clinic { get; set; } // Navigation Property



        [ForeignKey("Doctor")] // Foreign Key to Doctor
         [Required(ErrorMessage = "Please select a doctor.")]
        public long DoctorId { get; set; }
        public Doctor? Doctor { get; set; } // Navigation Property


        [DataType(DataType.Date)]
         [Required(ErrorMessage = "Please select an appointment date.")]
        public DateTime AppointmentDate { get; set; }

         [Required(ErrorMessage = "Please select an appointment time.")]
    [DataType(DataType.Time)]
        public TimeSpan AppointmentTime { get; set; }
        public string? PatientId { get; set; } // User ID from Identity
       
       
    }
}
