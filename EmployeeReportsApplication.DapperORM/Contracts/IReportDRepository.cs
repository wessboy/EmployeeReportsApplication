using EmployeeReportsApplication.DapperORM.Entities;
using EmployeeReportsApplication.DapperORM.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeReportsApplication.DapperORM.Contracts;

     public interface IReportDRepository
    {
       void Add(string tableName,Report report);
       void Update(string tableName,Report report);
       Report? FindOne(string tableName,string employeeName);
       IEnumerable<Report> FindAll(string tableName);
       void CreateTable(string tableName);
    }

