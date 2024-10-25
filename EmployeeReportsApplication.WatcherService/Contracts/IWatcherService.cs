using EmployeeReportsApplication.WatcherService.ValueObject;


namespace EmployeeReportsApplication.WatcherService.Contracts
{
     public interface IWatcherService
    {
        public void ConfigureWatcher();
        public void DisposeWatcher();
        public Queue<CsvFileInfo> AddedFiles { get; }

        //public event EventHandler<CsvFileCreatedArgs> CsvFileCreated;
    }
}
