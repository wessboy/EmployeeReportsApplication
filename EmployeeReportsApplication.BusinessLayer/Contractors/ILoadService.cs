using EmployeeReportsApplication.BusinessLayer.ValueObject;

namespace EmployeeReportsApplication.BusinessLayer.Contractors;
     public interface ILoadService
    {
        //void OnCsvFileCreated(object sender, CsvFileCreatedArgs args);
        //public event EventHandler<ReportLoadedArgs> ReportLoaded;
        //public void SubscribeToWatcher();
        List<DailyReport> LoadReportFromCSVFile(string filePath, string fileName);
     }

