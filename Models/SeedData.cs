using Microsoft.EntityFrameworkCore;

namespace HospitalApp.Models
{
    public static class SeedData
    {
// Class used to populate the database with initial data
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            // To get database
            StoreDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<StoreDbContext>();


            // If there are any pending migrations, apply them
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();   // Update database
            }

            if (!context.Clinics.Any()) // If there is no data in the 'Clinics' table, add the initial data
            {
                context.Clinics.AddRange(
                    new Clinic
                    {
                        Id = 1,   // Clinik ID
                        Name = "ENT"  // Ear Nose Throat Clinic
                    },
                    new Clinic
                    {
                        Id = 2,
                        Name = "Urology"
                    }, 
                    new Clinic
                    {
                        Id=3,
                        Name= "Cardiology"
                    },
                    new Clinic{
                        Id=4,
                        Name="Emergency Service"
                    },
                    new Clinic{
                        Id=5,
                        Name="Endocrinology"
                    },
                    new Clinic{
                        Id=6,
                        Name="Neurology"
                    }
                    ,
                    new Clinic{
                        Id=7,
                        Name="Orthopedics and Traumatology"
                    },
                    new Clinic{
                        Id=8,
                        Name="Dermatology"
                    },
                    new Clinic{
                        Id=9,
                        Name="Childhood Diseases"
                    },
                    new Clinic{
                        Id=10,
                        Name="Eye Diseases"
                    },
                    new Clinic{
                        Id=11,
                        Name="Chest Diseases"
                    },
                    new Clinic{
                        Id=12,
                        Name="General Surgery"
                    },
                    new Clinic{
                        Id=13,
                        Name="Internal Medicine"
                    },
                    new Clinic{
                        Id=14,
                        Name="Obstetrics and Gynaecology"
                    },
                    new Clinic{
                        Id=15,
                        Name="Plastic, Reconstructive and Aesthetic Surgery"
                    }



                );
                context.SaveChanges();   // Save changes to database
            }
// If there is no data in the 'Doctors' table, add the initial data
            if (!context.Doctors.Any())
            {
                context.Doctors.AddRange(
                    new Doctor
                    {
                        Name = "Muhammed DEMİRÖRS",   // Doctor Name
                        ClinicId = 1   // Clinic Id
                    },
                    new Doctor
                    {
                        Name = "Mustafa Hakan DİNÇKAL",
                        ClinicId = 2
                    },
                    new Doctor
                    {
                        Name = "Selçuk KURTARICI",
                        ClinicId = 3
                    },
                    new Doctor
                    {
                        Name = "Aylin KARDEŞ",
                        ClinicId = 4
                    },
                    new Doctor
                    {
                        Name = "Belgin SELAM",
                        ClinicId = 5
                    },
                    new Doctor
                    {
                        Name = "Hülya DEDE",
                        ClinicId = 6
                    },
                    new Doctor
                    {
                        Name = "Ahmet ALANAY",
                        ClinicId = 7
                    },
                    new Doctor
                    {
                        Name = "Özlem PATA",
                        ClinicId = 8
                    },
                    new Doctor
                    {
                        Name = "Ali KAYA",
                        ClinicId = 9
                    },
                    new Doctor
                    {
                        Name = "Şafak YILMAZ BARAN",
                        ClinicId = 10
                    },
                    new Doctor
                    {
                        Name = "Mustafa Kemal BATUR",
                        ClinicId = 11
                    },
                    new Doctor
                    {
                        Name = "Arda ALİŞAN",
                        ClinicId = 12
                    },
                    new Doctor
                    {
                        Name = "İlkut ÖZEL",
                        ClinicId = 13
                    },
                    new Doctor
                    {
                        Name = "Funda HELVACIOĞLU",
                        ClinicId = 14
                    },
                    new Doctor
                    {
                        Name = "AHMET CEMİL DALAY",
                        ClinicId = 15
                    },
                    new Doctor
                    {
                        Name = "Nalan KARADAĞ",
                        ClinicId = 1
                    }


                        );
                context.SaveChanges();   // Save changes to database
            }

             // Add some sample appointments for each clinic
            if (!context.Appointments.Any()) // Only add if there are no appointments
            {
                context.Appointments.AddRange(
                    new Appointment
                    {
                        DoctorId = 1, // Assuming DoctorId 1 exists
                        ClinicId = 1, // ENT Clinic
                        AppointmentDate = new DateTime(2024, 12, 25), // Example date
                        AppointmentTime = new TimeSpan(9, 0, 0), // Example time
                        PatientId = "d3d38181-66bc-4b42-b876-95a4e0780649"
                       
                    },
                    new Appointment
                    {
                        DoctorId = 2, // Assuming DoctorId 2 exists
                        ClinicId = 2, // Urology Clinic
                        AppointmentDate = new DateTime(2024, 12, 26), // Example date
                        AppointmentTime = new TimeSpan(10, 30, 0), // Example time
                        PatientId = "d3d38181-66bc-4b42-b876-95a4e0780649"
                       
                    },
                    new Appointment
                    {
                        DoctorId = 1, // Assuming DoctorId 1 exists
                        ClinicId = 1, // ENT Clinic
                        AppointmentDate = new DateTime(2024, 12, 30), // Example date
                        AppointmentTime = new TimeSpan(14, 0, 0), // Example time
                        PatientId = "d3d38181-66bc-4b42-b876-95a4e0780649"
                       
                    }, new Appointment
                    {
                        DoctorId = 9, // Assuming DoctorId 1 exists
                        ClinicId = 9, // ENT Clinic
                        AppointmentDate = new DateTime(2024, 12, 30), // Example date
                        AppointmentTime = new TimeSpan(14, 0, 0), // Example time
                        PatientId = "365c65fc-62bb-4f54-a4cb-c0a5704369a6"
                       
                    }, new Appointment
                    {
                        DoctorId = 14, // Assuming DoctorId 1 exists
                        ClinicId = 14, // ENT Clinic
                        AppointmentDate = new DateTime(2024, 01, 16), // Example date
                        AppointmentTime = new TimeSpan(14, 0, 0), // Example time
                        PatientId = "365c65fc-62bb-4f54-a4cb-c0a5704369a6"
                       
                    }, new Appointment
                    {
                        DoctorId = 12, // Assuming DoctorId 1 exists
                        ClinicId = 12, // ENT Clinic
                        AppointmentDate = new DateTime(2024, 01, 9), // Example date
                        AppointmentTime = new TimeSpan(11, 0, 0), // Example time
                        PatientId = "365c65fc-62bb-4f54-a4cb-c0a5704369a6"
                       
                    }
                );
                context.SaveChanges();   // Save changes to database
            }
        }
    }
}