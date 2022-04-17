using Microsoft.EntityFrameworkCore;
using WebApplicationWithAuth.Models;

namespace WebApplicationWithAuth.Data
{
    public class Northwind : DbContext
    {
  
        public DbSet<Customer> Customers { get; set; }


        public Northwind(DbContextOptions<Northwind> options)
            : base(options) { }

        protected override void OnConfiguring(
          DbContextOptionsBuilder optionsBuilder)
        {
            // to use Microsoft SQL Server, uncomment the following 
            /*optionsBuilder.UseSqlServer(
              @"Data Source=localhost;" +
              "Initial Catalog=Northwind;" +
              "Integrated Security=true;" +
              "MultipleActiveResultSets=true;");*/
            //(localdb)\mssqllocaldb
            // to use SQLite, uncomment the following 
            // string path = System.IO.Path.Combine(
            //   System.Environment.CurrentDirectory, "Northwind.db");
            // optionsBuilder.UseSqlite($"Filename={path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Customer>()
                .Property(c => c.CustomerID)
                .IsRequired()
                .HasMaxLength(5);

            modelBuilder.Entity<Customer>()
                .Property(c => c.CompanyName)
                .IsRequired()
                .HasMaxLength(40);

 
        }
    }
}