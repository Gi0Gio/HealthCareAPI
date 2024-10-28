using Microsoft.EntityFrameworkCore;
using giowebtestAPI.Models;

namespace giowebtestAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalPrescription> MedicalPrescriptions { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<BloodDonor> BloodDonors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointment)
                .HasForeignKey(a => a.DoctorId);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointment)
                .HasForeignKey(a => a.PatientId);

            modelBuilder.Entity<MedicalRecord>()
                .HasOne(mr => mr.Patient)
                .WithMany()
                .HasForeignKey(mr => mr.PatientId);

            modelBuilder.Entity<MedicalRecord>()
                .HasOne(mr => mr.Doctor)
                .WithMany(d => d.MedicalRecord)
                .HasForeignKey(mr => mr.DoctorId);

            modelBuilder.Entity<MedicalPrescription>()
                .HasOne(mp => mp.Patient)
                .WithMany(p => p.MedicalPrescription)
                .HasForeignKey(mp => mp.PatientId);

            modelBuilder.Entity<MedicalPrescription>()
                .HasOne(mp => mp.Doctor)
                .WithMany(d => d.MedicalPrescription)
                .HasForeignKey(mp => mp.DoctorId);

            base.OnModelCreating(modelBuilder);
        }
    }

}
