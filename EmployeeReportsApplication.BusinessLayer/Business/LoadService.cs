using CsvHelper;
using CsvHelper.Configuration;
using EmployeeReportsApplication.BusinessLayer.Contractors;
using EmployeeReportsApplication.BusinessLayer.ValueObject;
using EmployeeReportsApplication.WatcherService.Contracts;
using System.Globalization;


namespace EmployeeReportsApplication.BusinessLayer.Business;
     public class LoadService : ILoadService
    {
    private readonly IWatcherService _watcherService;

    public LoadService(IWatcherService watcherService)
        {
            _watcherService = watcherService;
            
        }

         public List<DailyReport> LoadReportFromCSVFile(string filePath , string fileName)
        {

            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader,CultureInfo.InvariantCulture);

            csv.Context.TypeConverterCache.AddConverter<bool>(new StringToBolleanConverter());
            csv.Context.RegisterClassMap<ReportMap>();

        
        
        
        
        List<DailyReport> dailyReports = csv.GetRecords<DailyReport>().ToList();

        reader.Close();

         return dailyReports;   
        //OnDailyReportLoaded(fileName,filePath,dailyReports); 

          
        }

        private CsvConfiguration ConfigureCSVFile()
        {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

           return configuration;
        }

       //public event EventHandler<ReportLoadedArgs> ReportLoaded;

      /* protected virtual void  OnDailyReportLoaded(string fileName,string filePath,List<DailyReport> dailyReports)
      {
         ReportLoaded?.Invoke(this,new ReportLoadedArgs { FileName = fileName,FilePath = filePath,DailyReports = dailyReports });  
      }*/

      
     /* public void OnCsvFileCreated(object sender, CsvFileCreatedArgs args) 
      { 
         LoadReportFromCSVFile(args.Path,args.Name);
      }*/

    /*public void SubscribeToWatcher()
    {
        _watcherService.CsvFileCreated += OnCsvFileCreated;
    }*/
}

