using Dapper;
using EmployeeReportsApplication.DapperORM.Contracts;
using EmployeeReportsApplication.DapperORM.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;


namespace EmployeeReportsApplication.DapperORM.Repositories;
public class ReportDRepository : IReportDRepository
{
    private readonly IDbConnection _dbConnection;
    public ReportDRepository(IConfiguration configuration)
    {
        _dbConnection = new NpgsqlConnection(configuration.GetConnectionString("PostgresConnection"));
    }

    public void Add(string tableName, Report report)
    {
        string query = @$"INSERT INTO {tableName}(Name,OnSiteDays,RemoteDays,LeaveDays)
                        VALUES(@Name,@OnSiteDays,@RemoteDays,@LeaveDays);";

        var parameters = new DynamicParameters();

        parameters.Add("Name",report.Name,DbType.String);
        parameters.Add("OnSiteDays", report.OnSiteDays, DbType.Int32);
        parameters.Add("RemoteDays",report.RemoteDays, DbType.Int32);
        parameters.Add("LeaveDays",report.LeaveDays, DbType.Int32);

        _dbConnection.Execute(query,parameters);
    }

    public void CreateTable(string tableName)
    {
        string query = @$"CREATE TABLE IF NOT EXISTS {tableName} (
                Id SERIAL PRIMARY KEY,
                Name VARCHAR(100),
                OnSiteDays INT,
                RemoteDays INT,
                LeaveDays INT
                );";

        _dbConnection.Execute(query);
    }
    public void Update(string tableName, Report report)
    {
        string query = @$"UPDATE {tableName} SET
                         Name = @Name,
                         OnSiteDays = @OnSiteDays,
                         RemoteDays = @RemoteDays,
                         LeaveDays = @LeaveDays
                         WHERE Id = @Id;";

        var parameters = new DynamicParameters();

        parameters.Add("Name", report.Name, DbType.String);
        parameters.Add("OnSiteDays", report.OnSiteDays, DbType.Int32);
        parameters.Add("RemoteDays", report.RemoteDays, DbType.Int32);
        parameters.Add("LeaveDays", report.LeaveDays, DbType.Int32);
        parameters.Add("Id",report.Id, DbType.Int32);

        _dbConnection.Execute(query, parameters);

    }

    IEnumerable<Report> IReportDRepository.FindAll(string tableName)
    {

        var tableExists = _dbConnection.QueryFirstOrDefault<bool>(
        @"SELECT EXISTS (
            SELECT FROM information_schema.tables 
            WHERE table_name = @TableName
        )", new { TableName = tableName });

        if (!tableExists) return [];

        
        var query = @$"SELECT * FROM {tableName};";

        IEnumerable<Report> reports = _dbConnection.Query<Report>(query).ToList();

        return reports;
    }

    Report? IReportDRepository.FindOne(string tableName, string employeeName)
    {
        string query = @$"SELECT * FROM {tableName}
                        WHERE Name = @Name";
        Report? report = _dbConnection.Query<Report>(query, new {Name = employeeName}).FirstOrDefault();

        return report;

    }
}

