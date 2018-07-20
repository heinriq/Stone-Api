using Microsoft.EntityFrameworkCore;
using StoneApi.Models;

namespace StoneApi.DAL
{
    public class TransactionContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public TransactionContext(DbContextOptions<TransactionContext> options) : base(options)
        {    
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {    
            modelBuilder.Entity<Transaction>().HasKey(o => o.TransactionId);
        }
    }
}