using EmployeeReportsApplication.BusinessLayer.BusinessEventArgs;
using EmployeeReportsApplication.BusinessLayer.ValueObject;
using EmployeeReportsApplication.WatcherService.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeReportsApplication.BusinessLayer.Contractors
{
     public interface ITransformationService
    {
        //public void OnDailyReportLoaded(object sender, ReportLoadedArgs args);
        //public void SubscribeToLoader();

        public void ExecuteTransformation(List<DailyReport> dailyReports, CsvFileInfo csvFileInfo);
    }
}
