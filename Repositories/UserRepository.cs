using InterneManagementSystem.Interfaces;
using InterneManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InterneManagementSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ImsContext _context;

        public UserRepository(ImsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(x => x.Role)
                .ToListAsync();
        }
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.UserId == id);
        }
        public async Task<User> AddUserAsync(User user)
        {
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return user;
        }
        public async Task<User?> UpdateUserAsync(User user)
        {
            var existing = await _context.Users.FindAsync(user.UserId);

            if (existing == null)
                return null;

            existing.FullName = user.FullName;
            existing.Username = user.Username;
            existing.Email = user.Email;
            existing.MobileNo = user.MobileNo;
            existing.RoleId = user.RoleId;
            existing.Designation = user.Designation;
            existing.IsActive = user.IsActive;

            await _context.SaveChangesAsync();

            return existing;
        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return false;

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return true;
        }

    }
}
