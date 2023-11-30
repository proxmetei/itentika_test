using itentika.Models;

namespace itentika.Services
{
    public static class Generator
    {
        public static Event GenerateEvent()
        {
            return new Event()
            {
                Id = Guid.NewGuid(),
                Type = (EventTypeEnum)new Random().Next(0, 4),
                Time = DateTime.Now
            };
        }
        public static Event GenerateEvent(int type)
        {
            return new Event()
            {
                Id = Guid.NewGuid(),
                Type = (EventTypeEnum)type,
                Time = DateTime.Now
            };
        }
    }
}
