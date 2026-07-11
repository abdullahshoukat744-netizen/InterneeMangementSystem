using Microsoft.EntityFrameworkCore;
using InterneManagementSystem.Interfaces;
using InterneManagementSystem.Models;

namespace InterneManagementSystem.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ImsContext _context;

        public RoleRepository(ImsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role?> GetRoleByIdAsync(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<Role> AddRoleAsync(Role role)
        {
            _context.Roles.Add(role);

            await _context.SaveChangesAsync();

            return role;
        }

        public async Task<Role?> UpdateRoleAsync(Role role)
        {
            var existing = await _context.Roles.FindAsync(role.RoleId);

            if (existing == null)
                return null;

            existing.RoleName = role.RoleName;
            existing.Description = role.Description;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
                return false;

            _context.Roles.Remove(role);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
