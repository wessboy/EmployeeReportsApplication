using EmployeeReportsApplication.BusinessLayer.BusinessEventArgs;
using EmployeeReportsApplication.WatcherService.WatcherEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeReportsApplication.BusinessLayer.Contractors;
     public interface ILoadService
    {
    void OnCsvFileCreated(object sender, CsvFileCreatedArgs args);

        public event EventHandler<ReportLoadedArgs> ReportLoaded;
    public void SubscribeToWatcher();
     }

