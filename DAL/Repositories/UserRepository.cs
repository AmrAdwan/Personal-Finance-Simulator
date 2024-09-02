// UserRepository.cs
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using finance.DAL.Interfaces;
using finance.Models;
using Microsoft.AspNetCore.Identity;

namespace finance.DAL.Repositories
{
  public class UserRepository : IUserRepository
  {
    private readonly FinanceDbContext _context;
    private readonly PasswordHasher<User> _passwordHasher;


    public UserRepository(FinanceDbContext context)
    {
      _context = context;
      _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
      return await _context.Users.ToListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
      return await _context.Users.FindAsync(id);
    }

    public async Task AddAsync(User entity)
    {
      await _context.Users.AddAsync(entity);
      await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User entity)
    {
      _context.Users.Update(entity);
      await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
      var user = await _context.Users.FindAsync(id);
      if (user != null)
      {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
      }
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
      return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
      var user = await GetUserByEmailAsync(email);
      if (user != null)
      {
        // Verify the hashed password
        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
        if (result == PasswordVerificationResult.Success)
        {
          return true;
        }
      }
      return false;
    }

    public async Task CreateUserAsync(User user, string password)
    {
      // user.PasswordHash = _passwordHasher.HashPassword(user, password);
      user.Password = _passwordHasher.HashPassword(user, password);
      await _context.Users.AddAsync(user);
      await _context.SaveChangesAsync();
    }
  }
}
