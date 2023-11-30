using itentika.Models;
using System.Runtime;

namespace itentika.Services
{
    public class GenerateService : BackgroundService
    {
        private readonly ILogger<GenerateService> _logger;

        public GenerateService(ILogger<GenerateService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(TimeSpan.FromSeconds( new Random().NextDouble() * 2));
            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                Event myEvent = Generator.GenerateEvent();
                _logger.LogInformation(
                    "GenerateService is working. Event: {eventStr}", myEvent.ToString());
                try
                {
                    RequestGenerator.SendRequest(myEvent);
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        "Error while sending requset from background. Exception: {ex}", ex);
                }
                timer.Period = TimeSpan.FromSeconds(new Random().NextDouble() * 2);
            }
        }
    }
}
