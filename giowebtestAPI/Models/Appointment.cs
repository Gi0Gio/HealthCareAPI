namespace giowebtestAPI.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }  // Pending, Confirmed, Completed, Canceled
        public string Type { get; set; }  // Consultation, Procedure, etc.
        public int ClinicId { get; set; }

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public Clinic Clinic { get; set; }
    }
}
