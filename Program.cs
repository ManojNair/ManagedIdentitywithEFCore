using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new BookDbContext();
            context.Books.Add(new Book() {Name = "Rust in Action"});
            context.SaveChanges();
            
        }
    }
}