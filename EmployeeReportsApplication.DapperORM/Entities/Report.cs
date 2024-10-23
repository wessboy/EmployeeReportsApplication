using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeReportsApplication.DapperORM.Entities;

     public class Report
    {
   
        public int Id { get; set; }
        public string Name { get; set; }
        public int OnSiteDays { get; set; }
        public int RemoteDays { get; set; }
        public int LeaveDays { get; set; }
}

