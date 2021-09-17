using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class BookDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlAuthenticationProvider.SetProvider(
                SqlAuthenticationMethod.ActiveDirectoryDeviceCodeFlow,
                new CustomAzureSqlAuthProvider());
            var sqlConnection = new SqlConnection("Server=tcp:delmenowdbserver.database.windows.net;Authentication=Active Directory Device Code Flow; Database=delmenowdb;");
            optionsBuilder.UseSqlServer(sqlConnection);
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(new List<Book>()
            {
                new Book() {Id = 1, Name = "CSharp in Action"},
                new Book() {Id = 2, Name = "Java in Action"}
            });
        }
    }
}