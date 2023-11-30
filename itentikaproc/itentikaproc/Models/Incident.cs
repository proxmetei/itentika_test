namespace itentikaproc.Models
{
    public enum IncidentTypeEnum
    {
        FirstType,
        SecondType
    }
    public class Incident
    {
        public Guid Id { get; set; }
        public IncidentTypeEnum Type { get; set; }
        public DateTimeOffset Time { get; set; }
        public List<Event> EventsList { get; set; }
        public override string ToString()
        {
            return $"Id: {Id},Type: {(int)Type},Time: {Time}";
        }
    }
}
