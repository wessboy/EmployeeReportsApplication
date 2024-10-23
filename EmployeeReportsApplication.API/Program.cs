using EmployeeReportsApplication.BusinessLayer.Business;
using EmployeeReportsApplication.BusinessLayer.Contractors;
using EmployeeReportsApplication.DapperORM.Contracts;
using EmployeeReportsApplication.DapperORM.Repositories;
using EmployeeReportsApplication.InternalWorker;
using EmployeeReportsApplication.WatcherService.Contracts;
using EmployeeReportsApplication.WatcherService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHostedService<WorkerService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IReportDRepository, ReportDRepository>();
builder.Services.AddScoped<ITransformationService, TransformationService>();
builder.Services.AddSingleton<ILoadService, LoadService>();
builder.Services.AddSingleton<IWatcherService, WatcherService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
