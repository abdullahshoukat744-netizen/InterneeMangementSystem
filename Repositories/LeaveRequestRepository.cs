using InterneManagementSystem.Interfaces;
using InterneManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InterneManagementSystem.Repositories
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ImsContext _context;

        public LeaveRequestRepository(ImsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LeaveRequest>> GetAllLeaveRequestsAsync()
        {
            return await _context.LeaveRequests
                .Include(l => l.Internee)
                .Include(l => l.ApprovedByNavigation)
                .ToListAsync();
        }

        public async Task<LeaveRequest?> GetLeaveRequestByIdAsync(int id)
        {
            return await _context.LeaveRequests
                .Include(l => l.Internee)
                .Include(l => l.ApprovedByNavigation)
                .FirstOrDefaultAsync(l => l.LeaveRequestId == id);
        }

        public async Task<LeaveRequest> AddLeaveRequestAsync(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Add(leaveRequest);

            await _context.SaveChangesAsync();

            return leaveRequest;
        }

        public async Task<LeaveRequest?> UpdateLeaveRequestAsync(LeaveRequest leaveRequest)
        {
            var existing = await _context.LeaveRequests.FindAsync(leaveRequest.LeaveRequestId);

            if (existing == null)
                return null;

            existing.InterneeId = leaveRequest.InterneeId;
            existing.StartDate = leaveRequest.StartDate;
            existing.EndDate = leaveRequest.EndDate;
            existing.Reason = leaveRequest.Reason;
            existing.Status = leaveRequest.Status;
            existing.ApprovedBy = leaveRequest.ApprovedBy;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteLeaveRequestAsync(int id)
        {
            var leaveRequest = await _context.LeaveRequests.FindAsync(id);

            if (leaveRequest == null)
                return false;

            _context.LeaveRequests.Remove(leaveRequest);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
