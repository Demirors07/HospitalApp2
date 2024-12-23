using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Models
{
    public class Clinic
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Doctor>? Doctors { get; set; } // Collection of doctors associated with the clinic.

    }
}