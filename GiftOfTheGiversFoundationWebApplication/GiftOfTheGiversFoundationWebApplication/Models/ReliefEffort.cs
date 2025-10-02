namespace GiftOfTheGiversFoundationWebApplication.Models
{
    public class ReliefEffort
    {
        public int ReliefEffortId { get; set; }   // PK
        public int IncidentAlertId { get; set; }  // FK
        public int VolunteerId { get; set; }      // FK
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }

        // Navigation properties
        public virtual IncidentAlert IncidentAlert { get; set; }
        public virtual Volunteer Volunteer { get; set; }
    }
}
