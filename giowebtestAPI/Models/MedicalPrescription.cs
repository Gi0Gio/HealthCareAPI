namespace giowebtestAPI.Models
{
    public class MedicalPrescription
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime IssueDate { get; set; }
        public string Description { get; set; }
        public string Dosage { get; set; }
        public int MedicationId { get; set; }

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public Medication Medication { get; set; }
    }

}
