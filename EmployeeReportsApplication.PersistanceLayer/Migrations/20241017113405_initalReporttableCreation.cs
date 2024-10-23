using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EmployeeReportsApplication.PersistanceLayer.Migrations;

    
    public partial class initalReporttableCreation : Migration
    {

    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Step 1: Create the main table with unique constraint
        migrationBuilder.Sql(@"
            CREATE TABLE Report (
                Id SERIAL,
                Name VARCHAR(100),
                OnSiteDays INT,
                RemoteDays INT,
                LeaveDays INT,
                DateTime TIMESTAMP NOT NULL,
                PRIMARY KEY (Id, DateTime)  -- Include DateTime in the primary key
            ) PARTITION BY RANGE (DateTime);
        ");

        // Step 2: Create partitions for each month of the year 2024
        for (int month = 1; month <= 12; month++)
        {
            string partitionName = $"Report_2024_{month:00}"; // Name format: Report_2024_01, Report_2024_02, etc.
            string startDate = $"2024-{month:00}-01"; // Start of the month
            string endDate = month < 12 ? $"2024-{month + 1:00}-01" : "2025-01-01"; // Start of the next month

            migrationBuilder.Sql($@"
                CREATE TABLE {partitionName} PARTITION OF Report 
                FOR VALUES FROM (TIMESTAMP '{startDate}') TO (TIMESTAMP '{endDate}');
            ");
        }

        // Repeat the above steps for other years if needed
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // Step 1: Drop the partitions for the year 2024
        for (int month = 1; month <= 12; month++)
        {
            string partitionName = $"Report_2024_{month:00}";
            migrationBuilder.Sql($"DROP TABLE IF EXISTS {partitionName};");
        }

        // Step 2: Drop the main table
        migrationBuilder.Sql("DROP TABLE IF EXISTS Report;");
    }

}

