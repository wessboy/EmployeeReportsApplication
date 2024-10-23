using EmployeeReportsApplication.BusinessLayer.Business;
using EmployeeReportsApplication.BusinessLayer.Contractors;
using EmployeeReportsApplication.DapperORM.Contracts;
using EmployeeReportsApplication.DapperORM.Repositories;
using EmployeeReportsApplication.PersistanceLayer.Contracts;
using EmployeeReportsApplication.PersistanceLayer.Repositories;
using EmployeeReportsApplication.WatcherService.Contracts;
using EmployeeReportsApplication.WatcherService.Services;
using EmployeeReportsApplication.WorkerService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

//Register Services
//builder.Services.AddScoped<IReportRepository,ReportRepository>();
builder.Services.AddScoped<IReportService,ReportService>();
builder.Services.AddScoped<IReportDRepository, ReportDRepository>();    
builder.Services.AddScoped<ITransformationService, TransformationService>();
builder.Services.AddSingleton<ILoadService,LoadService>();
builder.Services.AddSingleton<IWatcherService, WatcherService>();

/*builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"));
    
});*/


var host = builder.Build();

host.Run();
