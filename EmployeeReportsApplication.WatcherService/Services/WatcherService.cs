using EmployeeReportsApplication.WatcherService.Contracts;
using EmployeeReportsApplication.WatcherService.ValueObject;
using EmployeeReportsApplication.WatcherService.WatcherEventArgs;
using Microsoft.Extensions.Configuration;

namespace EmployeeReportsApplication.WatcherService.Services;
     public class WatcherService : IWatcherService
{
    private readonly FileSystemWatcher _watcher;
    private string _directoryPath;
        public WatcherService(IConfiguration configuration)
        {   
            _directoryPath = configuration.GetSection("Options:DirectoryPath").Value;
            _watcher = new FileSystemWatcher(_directoryPath);
        }

       
        public void ConfigureWatcher()
        {
             _watcher.EnableRaisingEvents = true;
             _watcher.Filter = "*.csv";
             _watcher.Created += OnCreated;
             
        }

   

      private  void OnCreated(object sender, FileSystemEventArgs e)
      {
        OnCsvFileCreated(e.Name!, e.FullPath);
      }

      public event EventHandler<CsvFileCreatedArgs> CsvFileCreated;

    protected virtual void OnCsvFileCreated(string Name,string path) 
    { 
         CsvFileCreated.Invoke(this, new CsvFileCreatedArgs{ Name = Name, Path = path });
    }

}

