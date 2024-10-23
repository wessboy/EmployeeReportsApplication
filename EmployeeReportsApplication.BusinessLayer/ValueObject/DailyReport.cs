using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeReportsApplication.BusinessLayer.ValueObject;
public class DailyReport
{
    public string Name { get; set; }
    public string?  Status { get; set; }
    public bool OnLeave { get; set; }
}

