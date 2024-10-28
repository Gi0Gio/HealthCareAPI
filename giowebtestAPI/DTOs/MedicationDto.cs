namespace giowebtestAPI.DTOs
{
    public class MedicationDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string RecommendedDosage { get; set; }
    }
}
