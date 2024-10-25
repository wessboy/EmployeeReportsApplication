using EmployeeReportsApplication.WatcherService.Contracts;
using EmployeeReportsApplication.WatcherService.ValueObject;
using EmployeeReportsApplication.WatcherService.WatcherEventArgs;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EmployeeReportsApplication.WatcherService.Services;
     public class WatcherService : IWatcherService
{
    private readonly FileSystemWatcher _watcher;
    private Queue<CsvFileInfo> _addedFiles { get; set; }
    private string _directoryPath;
        public WatcherService(IConfiguration configuration)
        {   
            _directoryPath = configuration.GetSection("Options:DirectoryPath").Value;
            _watcher = new FileSystemWatcher(_directoryPath);

            _addedFiles = new Queue<CsvFileInfo>();
        }

        public Queue<CsvFileInfo> AddedFiles { get { return _addedFiles; }}

        public void ConfigureWatcher()
        {
             _watcher.EnableRaisingEvents = true;
             _watcher.Filter = "*.csv";
             _watcher.Created += OnCreated;
        _watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;
             _watcher.InternalBufferSize= 64 * 1024;

    }

       public void DisposeWatcher()
       {
              _watcher?.Dispose();
       }

      private  void OnCreated(object sender, FileSystemEventArgs e)
      {
        //OnCsvFileCreated(e.Name!, e.FullPath);
        CsvFileInfo fileInfo = new CsvFileInfo { FullPath = e.FullPath, Name = e.Name };
        _addedFiles.Enqueue(fileInfo);  
      }

      //public event EventHandler<CsvFileCreatedArgs> CsvFileCreated;

   /* protected virtual void OnCsvFileCreated(string Name,string path) 
    { 
         CsvFileCreated.Invoke(this, new CsvFileCreatedArgs{ Name = Name, Path = path });
    }*/

}

