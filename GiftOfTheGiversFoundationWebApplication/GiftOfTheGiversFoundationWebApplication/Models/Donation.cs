namespace GiftOfTheGiversFoundationWebApplication.Models
{
    public class Donation
    {
        public int DonationId { get; set; }   // PK
        public int UserId { get; set; }       // FK
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        // Navigation property
        public virtual User User { get; set; }
    }
}
