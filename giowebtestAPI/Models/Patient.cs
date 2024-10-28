namespace giowebtestAPI.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Dni { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }

        public User User { get; set; }
        public ICollection<Appointment> Appointment { get; set; }
        public ICollection<MedicalPrescription> MedicalPrescription { get; set; }
    }
}
