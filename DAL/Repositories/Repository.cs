using finance.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace finance;
public class Repository<T> : IRepository<T> where T : class
{
  protected readonly FinanceDbContext _context;
  private readonly DbSet<T> _dbSet;

  public Repository(FinanceDbContext context)
  {
    _context = context;
    _dbSet = context.Set<T>();
  }

  public async Task<IEnumerable<T>> GetAllAsync()
  {
    return await _dbSet.ToListAsync();
  }

  public async Task<T> GetByIdAsync(int id)
  {
#pragma warning disable CS8603 // Possible null reference return.
    return await _dbSet.FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
  }

  public async Task AddAsync(T entity)
  {
    await _dbSet.AddAsync(entity);
    await _context.SaveChangesAsync();
  }

  public async Task UpdateAsync(T entity)
  {
    _dbSet.Update(entity);
    await _context.SaveChangesAsync();
  }

  public async Task DeleteAsync(int id)
  {
    var entity = await _dbSet.FindAsync(id);
    if (entity != null)
    {
      _dbSet.Remove(entity);
      await _context.SaveChangesAsync();
    }
  }
}