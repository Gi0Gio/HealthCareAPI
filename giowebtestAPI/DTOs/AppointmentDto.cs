namespace giowebtestAPI.DTOs
{
    public class AppointmentDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int ClinicId { get; set; }
    }

    public class PatientAppointmentDto
    {
        public DateTime AppointmentDate { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }

    public class AppointmentDetailDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; } // Nombre completo del doctor
        public DateTime AppointmentDate { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int ClinicId { get; set; }
    }

}
