namespace giowebtestAPI.Models
{
    public class BloodDonor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Identification { get; set; }
        public string BloodType { get; set; }
        public DateTime? LastDonationDate { get; set; }
    }

}
