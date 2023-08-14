using BemolChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace BemolChallenge.Data
{
    public class BemolContex : DbContext
    {
        public BemolContex(DbContextOptions<BemolContex> options) : base(options)
        {
            //Some options here
        }
        public DbSet <BemolObject> BemolObjects { get; set;}        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<BemolObject>()
                    .ToContainer("BemolObjects") // ToContainer
                    .HasPartitionKey(_ => _.Id); // Partition Key            
        }
    }
}
