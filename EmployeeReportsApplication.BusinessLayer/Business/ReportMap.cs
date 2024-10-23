using CsvHelper.Configuration;
using EmployeeReportsApplication.BusinessLayer.ValueObject;


namespace EmployeeReportsApplication.BusinessLayer.Business
{
     public class ReportMap : ClassMap<DailyReport>
    {
        public ReportMap() 
        {
            Map(r => r.Name).Index(0);
            Map(r => r.Status).Index(1);
            Map(r => r.OnLeave).TypeConverter<StringToBolleanConverter>()
                .Index(2);

        }
    }
}
