using EmployeeReportsApplication.BusinessLayer.Contractors;
using EmployeeReportsApplication.BusinessLayer.ValueObject;
using EmployeeReportsApplication.WatcherService.Contracts;
using EmployeeReportsApplication.WatcherService.ValueObject;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace EmployeeReportsApplication.InternalWorker;

    public class WorkerService : BackgroundService
    {
        private readonly ILogger<WorkerService> _logger;
        private readonly IWatcherService _watcherService;
        private readonly ILoadService _loadService;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private ITransformationService _transformationService;
        private IReportService _reportService;

        public WorkerService(ILogger<WorkerService> logger, IWatcherService watcherService, ILoadService loadService, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _watcherService = watcherService;
            
        _loadService = loadService;
            _serviceScopeFactory = serviceScopeFactory;
            using var scope = _serviceScopeFactory.CreateScope();
            _transformationService = scope.ServiceProvider.GetService<ITransformationService>()!;


    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _watcherService.ConfigureWatcher();
        InitalizeApplication();
        _logger.LogInformation("Worker is running");

        return base.StartAsync(cancellationToken);
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
                
                 

            while (!stoppingToken.IsCancellationRequested)
            {
                await ExecutETLProcessAsync();

                await Task.Delay(0, stoppingToken);
            }
             
            
        }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _watcherService.DisposeWatcher();
        _logger.LogInformation("Process ends");
        return base.StopAsync(cancellationToken);
    }
    private void InitalizeApplication()
        {
        using var scope = _serviceScopeFactory.CreateScope();  
        _reportService = scope.ServiceProvider.GetService<IReportService>()!;
        _reportService.IntializeDatabase();
        }

        private async Task ExecutETLProcessAsync()
        {

        List<DailyReport> extractedData = new List<DailyReport>();
        
                while(_watcherService.AddedFiles.TryDequeue(out var queuedFile))
        {
            try
            {
                CsvFileInfo addedFile = queuedFile;

                extractedData = await Task.Run(() => _loadService.LoadReportFromCSVFile(addedFile.FullPath, addedFile.Name));
                 await Task.Run(() => _transformationService.ExecuteTransformation(extractedData, addedFile));

            }
            catch (Exception ex)
            {

                _logger.LogError($"Error Processing File {queuedFile.Name}: {ex}");
            }
                    
                }
                
        }


    }

