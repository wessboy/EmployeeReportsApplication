﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeReportsApplication.WatcherService.ValueObject
{
     public class CsvFileInfo
    {
        public required string Name { get; set; }
        public required string FullPath { get; set; }
    }
}
