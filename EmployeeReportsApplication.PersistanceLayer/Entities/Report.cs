using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeReportsApplication.PersistanceLayer.Entities;
     public class Report
    {
      [Column("id")]
      public int Id { get; set; }
      [Column("name")]
      public string Name { get; set; }
      [Column("onsitedays")]
      public int OnSiteDays { get; set; }
      [Column("remotedays")]
      public int RemoteDays { get; set; }
      [Column("leavedays")]
      public int LeaveDays { get; set; }
      [Column("datetime")]
      public DateTime DateTime { get; set; }
    }

