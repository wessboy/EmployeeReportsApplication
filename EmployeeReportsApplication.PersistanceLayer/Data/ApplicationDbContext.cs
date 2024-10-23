using EmployeeReportsApplication.PersistanceLayer.Entities;
using Microsoft.EntityFrameworkCore;


namespace EmployeeReportsApplication.PersistanceLayer.Data;
     public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        
        }
    public DbSet<Report> report { get; set; }
}

