namespace giowebtestAPI.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialty { get; set; }
        public string PhoneNumber { get; set; }
        public string OfficeHours { get; set; }

        public User User { get; set; }
        public ICollection<MedicalPrescription> MedicalPrescription { get; set; }
        public ICollection<MedicalRecord> MedicalRecord { get; set; }
        public ICollection<Appointment> Appointment { get; set; }
    }
}
