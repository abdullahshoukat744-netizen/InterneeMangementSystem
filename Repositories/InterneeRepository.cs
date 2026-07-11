using InterneManagementSystem.Interfaces;
using InterneManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InterneManagementSystem.Repositories
{
    public class InterneeRepository : IInterneeRepository
    {
        private readonly ImsContext _context;

        public InterneeRepository(ImsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Internee>> GetAllInterneesAsync()
        {
            return await _context.Internees
                .Include(i => i.Department)
                .Include(i => i.University)
                .Include(i => i.Supervisor)
                .ToListAsync();
        }

        public async Task<Internee?> GetInterneeByIdAsync(int id)
        {
            return await _context.Internees
                .Include(i => i.Department)
                .Include(i => i.University)
                .Include(i => i.Supervisor)
                .FirstOrDefaultAsync(i => i.InterneeId == id);
        }

        public async Task<Internee> AddInterneeAsync(Internee internee)
        {
            internee.CreatedDate = DateTime.Now;

            _context.Internees.Add(internee);

            await _context.SaveChangesAsync();

            return internee;
        }

        public async Task<Internee?> UpdateInterneeAsync(Internee internee)
        {
            var existing = await _context.Internees.FindAsync(internee.InterneeId);

            if (existing == null)
                return null;

            existing.RegistrationNo = internee.RegistrationNo;
            existing.Cnic = internee.Cnic;
            existing.FirstName = internee.FirstName;
            existing.LastName = internee.LastName;
            existing.FatherName = internee.FatherName;
            existing.Gender = internee.Gender;
            existing.DateOfBirth = internee.DateOfBirth;
            existing.Address = internee.Address;
            existing.MobileNo = internee.MobileNo;
            existing.Email = internee.Email;
            existing.UniversityId = internee.UniversityId;
            existing.DegreeProgram = internee.DegreeProgram;
            existing.Semester = internee.Semester;
            existing.Cgpa = internee.Cgpa;
            existing.JoiningDate = internee.JoiningDate;
            existing.InternshipMonths = internee.InternshipMonths;
            existing.EndDate = internee.EndDate;
            existing.DepartmentId = internee.DepartmentId;
            existing.SupervisorId = internee.SupervisorId;
            existing.Status = internee.Status;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteInterneeAsync(int id)
        {
            var internee = await _context.Internees.FindAsync(id);

            if (internee == null)
                return false;

            _context.Internees.Remove(internee);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
