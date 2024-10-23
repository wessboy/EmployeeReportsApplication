using EmployeeReportsApplication.BusinessLayer.Contractors;
using EmployeeReportsApplication.DapperORM.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeReportsApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("all/{month}",Name = "Reports")]
        public ActionResult<IEnumerable<Report>> GetReports(string month)
        {
            IEnumerable<Report> reports =  _reportService.GetAll(month);

            if(!reports.Any()) return NotFound($"No Reports Exists for month : {month}");

            return Ok(reports);
        }
    }
}
