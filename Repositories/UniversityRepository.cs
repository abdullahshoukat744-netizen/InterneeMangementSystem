using InterneManagementSystem.Interfaces;
using InterneManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InterneManagementSystem.Repositories
{
    public class UniversityRepository : IUniversityRepository
    
    {
        private readonly ImsContext _context;

        public UniversityRepository(ImsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<University>> GetAllUniversitiesAsync()
        {
            return await _context.Universities.ToListAsync();
        }

        public async Task<University?> GetUniversityByIdAsync(int id)
        {
            return await _context.Universities.FindAsync(id);
        }

        public async Task<University> AddUniversityAsync(University university)
        {
            _context.Universities.Add(university);

            await _context.SaveChangesAsync();

            return university;
        }

        public async Task<University?> UpdateUniversityAsync(University university)
        {
            var existingUniversity = await _context.Universities.FindAsync(university.UniversityId);

            if (existingUniversity == null)
                return null;

            existingUniversity.UniversityName = university.UniversityName;
            existingUniversity.City = university.City;

            await _context.SaveChangesAsync();

            return existingUniversity;
        }

        public async Task<bool> DeleteUniversityAsync(int id)
        {
            var university = await _context.Universities.FindAsync(id);

            if (university == null)
                return false;

            _context.Universities.Remove(university);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
