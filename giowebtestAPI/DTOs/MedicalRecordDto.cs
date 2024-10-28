namespace giowebtestAPI.DTOs
{
    public class MedicalRecordDto
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime RecordDate { get; set; }
        public string Diagnosis { get; set; }
        public string ProcedurePerformed { get; set; }
        public string Notes { get; set; }
        public int MedicationId { get; set; }
    }
}
