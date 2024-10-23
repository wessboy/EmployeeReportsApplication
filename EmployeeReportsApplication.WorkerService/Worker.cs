using EmployeeReportsApplication.BusinessLayer.Contractors;
using EmployeeReportsApplication.WatcherService.Contracts;


namespace EmployeeReportsApplication.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IWatcherService _watcherService;
        private readonly ILoadService _loadService;
       private readonly IServiceScopeFactory _serviceScopeFactory;
       private  ITransformationService _transformationService;

        public Worker(ILogger<Worker> logger,IWatcherService watcherService,ILoadService loadService,IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _watcherService = watcherService;
            _loadService = loadService;
            _serviceScopeFactory = serviceScopeFactory;
            
            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _watcherService.ConfigureWatcher();
            _loadService.SubscribeToWatcher();
            using var scope = _serviceScopeFactory.CreateScope();
            _transformationService = scope.ServiceProvider.GetService<ITransformationService>()!;
            _transformationService.SubscribeToLoader();

            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    

                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
