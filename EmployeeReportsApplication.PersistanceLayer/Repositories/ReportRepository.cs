using EmployeeReportsApplication.PersistanceLayer.Contracts;
using EmployeeReportsApplication.PersistanceLayer.Data;
using EmployeeReportsApplication.PersistanceLayer.Entities;


namespace EmployeeReportsApplication.PersistanceLayer.Repositories
{
    public class ReportRepository : BaseRepository<Report>,IReportRepository
    {
        
        public ReportRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
             
        }

    }
}
