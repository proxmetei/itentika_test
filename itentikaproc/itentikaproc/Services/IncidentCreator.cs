using itentikaproc.Models;

namespace itentikaproc.Services
{
    public static class IncidentCreator
    {
        public static Incident GenerateEvent(int type)
        {
            return new Incident()
            {
                Id = Guid.NewGuid(),
                Type = (IncidentTypeEnum)type,
                Time = DateTime.Now
            };
        }
    }
}
