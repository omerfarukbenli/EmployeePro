using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI
{
    public class DataContext : DbContext
    {

        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }


        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<EmployeeModel> EmployeeModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeModel>().ToTable("UserModels");
            modelBuilder.Entity<EmployeeModel>().ToTable("EmployeeModels");
        }

    }
}
