﻿using EmployeeReportsApplication.BusinessLayer.BusinessEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeReportsApplication.BusinessLayer.Contractors
{
     public interface ITransformationService
    {
        public void OnDailyReportLoaded(object sender, ReportLoadedArgs args);
        public void SubscribeToLoader();
    }
}