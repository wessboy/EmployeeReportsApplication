using EmployeeReportsApplication.WatcherService.WatcherEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeReportsApplication.WatcherService.Contracts
{
     public interface IWatcherService
    {
        void ConfigureWatcher();

        public event EventHandler<CsvFileCreatedArgs> CsvFileCreated;
    }
}
