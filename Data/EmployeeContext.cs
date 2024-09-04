using EmployeeServer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace EmployeeServer.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        public List<Employee> GetEmployeesByDept(string dept)
        {
            return Employees.FromSqlRaw("EXEC GetEmployeeDetails @p0",dept).ToList();
        }
    }
}
