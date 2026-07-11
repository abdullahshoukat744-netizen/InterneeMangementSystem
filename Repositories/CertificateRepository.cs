using InterneManagementSystem.Interfaces;
using InterneManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InterneManagementSystem.Repositories
{
    public class CertificateRepository : ICertificateRepository
    {
        private readonly ImsContext _context;

        public CertificateRepository(ImsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Certificate>> GetAllCertificatesAsync()
        {
            return await _context.Certificates
                .Include(c => c.Internee)
                .ToListAsync();
        }

        public async Task<Certificate?> GetCertificateByIdAsync(int id)
        {
            return await _context.Certificates
                .Include(c => c.Internee)
                .FirstOrDefaultAsync(c => c.CertificateId == id);
        }

        public async Task<Certificate> AddCertificateAsync(Certificate certificate)
        {
            // Automatically set Issue Date if not provided
            certificate.IssueDate ??= DateOnly.FromDateTime(DateTime.Now);

            _context.Certificates.Add(certificate);

            await _context.SaveChangesAsync();

            return certificate;
        }

        public async Task<Certificate?> UpdateCertificateAsync(Certificate certificate)
        {
            var existing = await _context.Certificates
                .FindAsync(certificate.CertificateId);

            if (existing == null)
                return null;

            existing.InterneeId = certificate.InterneeId;
            existing.CertificateType = certificate.CertificateType;
            existing.IssueDate = certificate.IssueDate;
            existing.Qrcode = certificate.Qrcode;
            existing.CertificatePath = certificate.CertificatePath;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteCertificateAsync(int id)
        {
            var certificate = await _context.Certificates
                .FindAsync(id);

            if (certificate == null)
                return false;

            _context.Certificates.Remove(certificate);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
