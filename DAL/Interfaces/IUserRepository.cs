using finance.Models;
using System.Threading.Tasks;
namespace finance.DAL.Interfaces
{
  public interface IUserRepository : IRepository<User>
  {
    Task<User?> GetUserByEmailAsync(string email);
    Task<bool> LoginAsync(string email, string password);
    Task CreateUserAsync(User user, string password);

  }
}