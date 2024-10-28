namespace giowebtestAPI.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime RecordDate { get; set; }
        public string Diagnosis { get; set; }
        public int MedicationId { get; set; }
        public string ProcedurePerformed { get; set; }
        public string Notes { get; set; }

        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public Medication Medication { get; set; }
    }
}
