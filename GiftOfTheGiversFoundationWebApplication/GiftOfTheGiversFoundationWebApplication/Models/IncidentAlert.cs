namespace GiftOfTheGiversFoundationWebApplication.Models
{
    public class IncidentAlert
    {
        public int IncidentAlertId { get; set; }  // PK
        public int UserId { get; set; }           // FK
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual ICollection<ReliefEffort> ReliefEfforts { get; set; }
    }
}
