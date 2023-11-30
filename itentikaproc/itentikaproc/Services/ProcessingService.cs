using itentikaproc.Models;
using itentikaproc.Repositories;
using Microsoft.EntityFrameworkCore;

namespace itentikaproc.Services
{
    public class ProcessingService
    {
        private readonly ILogger<ProcessingService> _logger;
        private Timer? _timer = null;
        private List<Event> _events;
        private readonly IServiceScopeFactory _scopeFactory;
        private const int waitTime = 5;

        public ProcessingService(ILogger<ProcessingService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _events = new List<Event>();
            _scopeFactory = scopeFactory;
        }
        public void processEvent(Event curEvent)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ProcessingRepo>();
                if ((int)curEvent.Type == 2)
                {
                    if (_timer != null)
                        DoWork(null);
                    _events.Add(curEvent);
                    _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(waitTime), TimeSpan.Zero);
                }
                else if ((int)curEvent.Type == 1)
                {
                    _events.Add(curEvent);
                    if (_timer != null)
                    {
                        Incident incedent = IncidentCreator.GenerateEvent(2);
                        incedent.EventsList = _events;
                        repo.addProcess(incedent);
                        _events.Clear();
                        _timer?.Dispose();
                        _timer = null;
                    }
                    else
                    {
                        Incident incedent = IncidentCreator.GenerateEvent(1);
                        incedent.EventsList = _events;
                        repo.addProcess(incedent);
                        _events.Clear();
                    }
                }
            }
            _logger.LogInformation("ProcessingService is working. Timer is not waiting : {eventStr}", _timer != null);
        }
        private void DoWork(object? state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ProcessingRepo>();
                Incident incedent = IncidentCreator.GenerateEvent(1);
                _events.FirstOrDefault().Type = (EventTypeEnum)1;
                incedent.EventsList = _events;
                repo.addProcess(incedent);
                _events.Clear();
                _timer?.Dispose();
                _timer = null;
            }
        }
    }
}
