namespace GiftOfTheGiversFoundationWebApplication.Models
{
    public class Volunteer
    {
        public int VolunteerId { get; set; }  // PK
        public int UserId { get; set; }       // FK
        public string FirstName { get; set; }
        public string EmailAddress { get; set; }

        // Navigation properties
        public virtual User User { get; set; } // 1:1 relationship
        public virtual ICollection<ReliefEffort> ReliefEfforts { get; set; }
    }
}
