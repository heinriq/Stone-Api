using Microsoft.EntityFrameworkCore;
using StoneChallange.DAL;
using StoneChallange.Models;

namespace StoneChallange.DAL {
    public class TransacaoContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        public TransacaoContext(DbContextOptions<TransacaoContext> options) : base(options)
        {    
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {    
            modelBuilder.Entity<Transaction>().HasKey(o => o.TransactionId);
        }
    }
}