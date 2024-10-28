namespace giowebtestAPI.DTOs
{
    public class MedicalPrescriptionDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime IssueDate { get; set; }
        public string Description { get; set; }
        public string Dosage { get; set; }
        public int MedicationId { get; set; }
    }
}
