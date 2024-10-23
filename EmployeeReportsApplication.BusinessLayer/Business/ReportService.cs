 using EmployeeReportsApplication.BusinessLayer.Contractors;
using EmployeeReportsApplication.DapperORM.Contracts;
using EmployeeReportsApplication.DapperORM.Entities;

namespace EmployeeReportsApplication.BusinessLayer.Business;
public class ReportService : IReportService
{
    private readonly IReportDRepository _reportDRepository;

    public ReportService(IReportDRepository reportDRepository)
    {
        _reportDRepository = reportDRepository;
    }

    private string CurrentTable { get; set; }

    public void AddReport(Report report)
    {
          if(!string.IsNullOrEmpty(CurrentTable)) 
        {
            _reportDRepository.Add(CurrentTable, report);
        }
    }

    public IEnumerable<Report> GetAll(string month)
    {
        

        IEnumerable<Report> reports = _reportDRepository.FindAll("report_" + month);

        return reports;
    }

    public Report? GetByName(string name)
    {
        if(string.IsNullOrEmpty(CurrentTable)) return null;


        Report? report = _reportDRepository.FindOne(CurrentTable, name);    
        
         return report;
    }

    public void UpdateReport(Report report)
    {
        if(!string.IsNullOrEmpty(CurrentTable))
        {
            _reportDRepository.Update(CurrentTable, report);
        }
    }

    public void UpdateTableName(string tableName)
    {
        if(string.IsNullOrEmpty(CurrentTable) || !CurrentTable.Equals(tableName))
        {
            CurrentTable = tableName;
            CreateNewTable(CurrentTable);
        }
              
    }

    private void CreateNewTable(string tableName)
    {
        _reportDRepository.CreateTable(tableName);
    }
}

