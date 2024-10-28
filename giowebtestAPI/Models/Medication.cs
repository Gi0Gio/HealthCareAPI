namespace giowebtestAPI.Models
{
    public class Medication
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string RecommendedDosage { get; set; }

        //public MedicalRegister MedicalRegister { get; set; }
    }
}
