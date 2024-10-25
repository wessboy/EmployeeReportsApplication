using EmployeeReportsApplication.DapperORM.Entities;

namespace EmployeeReportsApplication.BusinessLayer.Contractors
{
     public interface IReportService
    {
        IEnumerable<Report> GetAll(string month);
        Report? GetByName(string name);
        void AddReport(Report report);
        void UpdateReport(Report report);
        void UpdateTableName(string tableName);
        void IntializeDatabase();
    }
}
