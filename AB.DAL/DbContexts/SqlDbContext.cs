using AB.Entities.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AB.DAL.DbContexts
{
    public class SqlDbContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Unite> Unite { get; set; }
        
        /// <summary>
        /// dotnet ef komutu icin gerekli contructer
        /// </summary>
        public SqlDbContext()
        {
            
        }

        /// <summary>
        /// Burasi MVC projesi icerisindeki Services Container'i icin gerekli constructer
        /// </summary>
        /// <param name="options"></param>
        public SqlDbContext(DbContextOptions<SqlDbContext> options) :base(options) 
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           			optionsBuilder.UseNpgsql("Server = 127.0.0.1; Port = 5432; Database = ABCompany; User Id = postgres; Password = 1234;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load("AB.Entities"));
        }
    }
}
