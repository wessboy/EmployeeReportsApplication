using EmployeeReportsApplication.BusinessLayer.ValueObject;

namespace EmployeeReportsApplication.BusinessLayer.BusinessEventArgs;
     public class ReportLoadedArgs : EventArgs
    {
        public ReportLoadedArgs()
        {
              DailyReports = new List<DailyReport>();
        }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public List<DailyReport> DailyReports { get; set; }
    }

