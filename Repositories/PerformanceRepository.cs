using InterneManagementSystem.Interfaces;
using InterneManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InterneManagementSystem.Repositories
{
    public class PerformanceRepository: IPerformanceRepository
    {
        private readonly ImsContext _context;

        public PerformanceRepository(ImsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PerformanceEvaluation>> GetAllPerformanceAsync()
        {
            return await _context.PerformanceEvaluations
                .Include(p => p.Internee)
                .ToListAsync();
        }

        public async Task<PerformanceEvaluation?> GetPerformanceByIdAsync(int id)
        {
            return await _context.PerformanceEvaluations
                .Include(p => p.Internee)
                .FirstOrDefaultAsync(p => p.EvaluationId == id);
        }

        public async Task<PerformanceEvaluation> AddPerformanceAsync(PerformanceEvaluation performance)
        {
            // Calculate Total Marks
            performance.TotalMarks =
                (performance.Discipline ?? 0) +
                (performance.Attendance ?? 0) +
                (performance.Communication ?? 0) +
                (performance.TechnicalSkills ?? 0) +
                (performance.Teamwork ?? 0);

            // Assign Grade
            performance.Grade = GetGrade(performance.TotalMarks ?? 0);

            _context.PerformanceEvaluations.Add(performance);

            await _context.SaveChangesAsync();

            return performance;
        }

        public async Task<PerformanceEvaluation?> UpdatePerformanceAsync(PerformanceEvaluation performance)
        {
            var existing = await _context.PerformanceEvaluations
                .FindAsync(performance.EvaluationId);

            if (existing == null)
                return null;

            existing.InterneeId = performance.InterneeId;
            existing.Discipline = performance.Discipline;
            existing.Attendance = performance.Attendance;
            existing.Communication = performance.Communication;
            existing.TechnicalSkills = performance.TechnicalSkills;
            existing.Teamwork = performance.Teamwork;
            existing.EvaluationDate = performance.EvaluationDate;

            existing.TotalMarks =
                (performance.Discipline ?? 0) +
                (performance.Attendance ?? 0) +
                (performance.Communication ?? 0) +
                (performance.TechnicalSkills ?? 0) +
                (performance.Teamwork ?? 0);

            existing.Grade = GetGrade(existing.TotalMarks ?? 0);

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeletePerformanceAsync(int id)
        {
            var performance = await _context.PerformanceEvaluations.FindAsync(id);

            if (performance == null)
                return false;

            _context.PerformanceEvaluations.Remove(performance);

            await _context.SaveChangesAsync();

            return true;
        }

        private string GetGrade(int marks)
        {
            if (marks >= 90)
                return "Excellent";

            if (marks >= 80)
                return "Very Good";

            if (marks >= 70)
                return "Good";

            if (marks >= 60)
                return "Satisfactory";

            return "Needs Improvement";
        }
    }
}

