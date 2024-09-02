using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using finance.Models;

public class FinanceDbContext : IdentityDbContext<User> // Inherit from IdentityDbContext
{
  public FinanceDbContext(DbContextOptions<FinanceDbContext> options)
      : base(options)
  { }


  public DbSet<FinancialProfile> FinancialProfiles { get; set; }
  public DbSet<Transaction> Transactions { get; set; }
  public DbSet<SimulationResult> SimulationResults { get; set; }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);

    builder.Entity<SimulationResult>(entity =>
        {
            entity.ToTable("SimulationResults"); 
        });
  }
}
