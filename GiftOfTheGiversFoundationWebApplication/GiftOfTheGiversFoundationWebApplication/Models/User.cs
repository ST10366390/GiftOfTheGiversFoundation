namespace GiftOfTheGiversFoundationWebApplication.Models
{
    public class User
    {
        public int UserId { get; set; }  // PK
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        // Navigation properties
        public virtual Volunteer Volunteer { get; set; } // 1:1 with Volunteer
        public virtual ICollection<Donation> Donations { get; set; } // 1:* with Donations
        public virtual ICollection<IncidentAlert> IncidentAlerts { get; set; }
    }
}
