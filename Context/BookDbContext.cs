using BooksApiYayin.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksApiYayin.Context
{
    public class BookDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MSA; database=BookApiYayin; Integrated Security=True; TrustServerCertificate=True");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<RentedBook> RentedBooks { get; set; }
    }
}
