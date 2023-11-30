namespace itentikaproc.Models
{
    public enum EventTypeEnum
    { 
        FirstType,
        SecondType,
        ThirdType,
        FourthType
    }
    public class Event
    {
       public Guid Id { get; set; }
       public  EventTypeEnum Type { get; set; }
       public DateTime Time {  get; set; }
       public override string ToString()
       {
           return $"Id: {Id},Type: {(int)Type},Time: {Time}";
       }
    }
}
