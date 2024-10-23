using EmployeeReportsApplication.BusinessLayer.BusinessEventArgs;
using EmployeeReportsApplication.BusinessLayer.Contractors;
using EmployeeReportsApplication.BusinessLayer.ValueObject;
using EmployeeReportsApplication.DapperORM.Entities;
using Microsoft.Extensions.Configuration;

namespace EmployeeReportsApplication.BusinessLayer.Business;

     public class TransformationService : ITransformationService
    {
        private readonly IReportService _reportService;
        private readonly ILoadService _loadService;

    public TransformationService(IReportService reportService,ILoadService loadService,IConfiguration configuration)

         
        {
            _reportService = reportService;
            _loadService = loadService;
            TreatedReportsPath = configuration.GetSection("Options:TreatedReportsPath").Value!;
            UntreatedReportsPath = configuration.GetSection("Options:UntreatedReportsPath").Value!;

        }

      private string TreatedReportsPath { get; set; }
      private string UntreatedReportsPath {  set; get; }


         private void TransformDailyReportToReport(List<DailyReport> dailyReports)
        {
             
            foreach(var dailyReport in dailyReports)
            {
               Report? report =  _reportService.GetByName(dailyReport.Name);
               if(report == null)
            {
                Report newReport = MapDailyReportToNewReport(dailyReport);
                _reportService.AddReport(newReport);
            }

            else
            {
                  Report updatedReport = MapDailyReportToExistedReport(dailyReport, report);
                    _reportService.UpdateReport(updatedReport);
            }
                  
                 
            }
        }

       private void ExtractDateFromFileName(string fileName)
       {
           string extractedDate = fileName.Substring(fileName.IndexOf('_')+1,10);

            DateTime currentDate = DateTime.Parse(extractedDate);

           FormatTableName(currentDate);
       }

      private void FormatTableName(DateTime date)
    {
        string month = date.ToString("MMMM");
        string TableName = "Report_" + month;

        _reportService.UpdateTableName(TableName);
    }
    
        private Report MapDailyReportToNewReport(DailyReport dailyReport)
        {
               
         Report newReport = new Report {Name = dailyReport.Name, LeaveDays = 1,RemoteDays = 0,OnSiteDays = 0};
                
                 if(!dailyReport.OnLeave)
                {
                   newReport.LeaveDays = 0;

                   if (dailyReport.Status == "Remote") newReport.RemoteDays = 1;

                   else newReport.OnSiteDays = 1;
                }

                 return newReport;
        }

        private Report MapDailyReportToExistedReport(DailyReport dailyReport, Report report)
        {
            if (dailyReport.OnLeave) 
               {
                    report.LeaveDays += 1;
                    return report;
               }

              if(dailyReport.Status == "Remote") report.RemoteDays += 1;
              else 
                 report.OnSiteDays += 1;


              return report;
              
        }

        private bool IsDataValid(IEnumerable<DailyReport> dailyReports,string filePath) 
        { 
             foreach(var dailyReport in dailyReports)
        {
             if((dailyReport.Status == "N/A" && !dailyReport.OnLeave) || (dailyReport.Status != "N/A" && dailyReport.OnLeave))
            {  
                return false;
            }
        } 
              return true;
        }

       private void MoveFileToTreatedReportsFolder(string filePath,string fileName) 
      {
        try
        {
            File.Move(filePath, TreatedReportsPath + $"\\{fileName}");
        }
        catch (IOException)
        {

            throw;
        }
         
      }
      
      private void  MoveFileToUntreatedReportsFolder(string filePath,string fileName)
    {
        try
        {
            File.Move(filePath, UntreatedReportsPath + $"\\{fileName}");
        }
        catch (IOException)
        {

            throw;
        }
        
    }

    public void OnDailyReportLoaded(object sender, ReportLoadedArgs args)
    {  
         if(IsDataValid(args.DailyReports,args.FilePath)) 
        {
            ExtractDateFromFileName(args.FileName);
            TransformDailyReportToReport(args.DailyReports);
            MoveFileToTreatedReportsFolder(args.FilePath,args.FileName);

        }
        else
        {
            MoveFileToUntreatedReportsFolder(args.FilePath,args.FileName);
        }
        
    }

    public void SubscribeToLoader()
    {
        _loadService.ReportLoaded += OnDailyReportLoaded;
    }
}



