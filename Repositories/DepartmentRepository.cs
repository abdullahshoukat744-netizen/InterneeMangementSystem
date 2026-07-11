using InterneManagementSystem.Interfaces;
using InterneManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InterneManagementSystem.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ImsContext _context;

        public DepartmentRepository(ImsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department?> GetDepartmentByIdAsync(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task<Department> AddDepartmentAsync(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return department;
        }

        public async Task<Department?> UpdateDepartmentAsync(Department department)
        {
            var existingDepartment = await _context.Departments.FindAsync(department.DepartmentId);

            if (existingDepartment == null)
                return null;

            existingDepartment.DepartmentName = department.DepartmentName;
            existingDepartment.Wing = department.Wing;
            existingDepartment.Section = department.Section;
            existingDepartment.Description = department.Description;

            await _context.SaveChangesAsync();

            return existingDepartment;
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
                return false;

            _context.Departments.Remove(department);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
