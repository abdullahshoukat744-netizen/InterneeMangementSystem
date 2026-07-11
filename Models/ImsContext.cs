using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InterneManagementSystem.Models;

public partial class ImsContext : DbContext
{
    public ImsContext()
    {
    }

    public ImsContext(DbContextOptions<ImsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Certificate> Certificates { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Internee> Internees { get; set; }

    public virtual DbSet<LeaveRequest> LeaveRequests { get; set; }

    public virtual DbSet<PerformanceEvaluation> PerformanceEvaluations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<University> Universities { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WeeklyProgress> WeeklyProgresses { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-DLGEHS4\\SQLEXPRESS;Database=IMS;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PK__Attendan__8B69261C3A46D8AD");

            entity.ToTable("Attendance");

            entity.HasIndex(e => new { e.InterneeId, e.AttendanceDate }, "UX_Attendance").IsUnique();

            entity.Property(e => e.Remarks).HasMaxLength(300);
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Internee).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.InterneeId)
                .HasConstraintName("FK__Attendanc__Inter__4E88ABD4");
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.AuditLogId).HasName("PK__AuditLog__EB5F6CBDCE018E55");

            entity.Property(e => e.Action).HasMaxLength(100);
            entity.Property(e => e.ActionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(50)
                .HasColumnName("IPAddress");
            entity.Property(e => e.TableName).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__AuditLogs__UserI__5EBF139D");
        });

        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.HasKey(e => e.CertificateId).HasName("PK__Certific__BBF8A7C1C088DF40");

            entity.Property(e => e.CertificatePath).HasMaxLength(500);
            entity.Property(e => e.CertificateType).HasMaxLength(50);
            entity.Property(e => e.Qrcode)
                .HasMaxLength(300)
                .HasColumnName("QRCode");

            entity.HasOne(d => d.Internee).WithMany(p => p.Certificates)
                .HasForeignKey(d => d.InterneeId)
                .HasConstraintName("FK__Certifica__Inter__5AEE82B9");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BED6E53A88C");

            entity.Property(e => e.DepartmentName).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.Section).HasMaxLength(100);
            entity.Property(e => e.Wing).HasMaxLength(100);
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__Document__1ABEEF0F66E6CC1C");

            entity.Property(e => e.DocumentType).HasMaxLength(50);
            entity.Property(e => e.FileName).HasMaxLength(200);
            entity.Property(e => e.FilePath).HasMaxLength(500);
            entity.Property(e => e.UploadDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Internee).WithMany(p => p.Documents)
                .HasForeignKey(d => d.InterneeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Documents__Inter__4BAC3F29");
        });

        modelBuilder.Entity<Internee>(entity =>
        {
            entity.HasKey(e => e.InterneeId).HasName("PK__Internee__F883EBCF3A87969D");

            entity.HasIndex(e => e.RegistrationNo, "UQ__Internee__6EF5E04258FF3A9B").IsUnique();

            entity.HasIndex(e => e.Cnic, "UQ__Internee__AA570FD44AF29019").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.Cgpa)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("CGPA");
            entity.Property(e => e.Cnic)
                .HasMaxLength(15)
                .HasColumnName("CNIC");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DegreeProgram).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FatherName).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(20);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MobileNo).HasMaxLength(20);
            entity.Property(e => e.RegistrationNo).HasMaxLength(30);
            entity.Property(e => e.Status).HasMaxLength(30);

            entity.HasOne(d => d.Department).WithMany(p => p.Internees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Internees__Depar__46E78A0C");

            entity.HasOne(d => d.Supervisor).WithMany(p => p.Internees)
                .HasForeignKey(d => d.SupervisorId)
                .HasConstraintName("FK__Internees__Super__47DBAE45");

            entity.HasOne(d => d.University).WithMany(p => p.Internees)
                .HasForeignKey(d => d.UniversityId)
                .HasConstraintName("FK__Internees__Unive__45F365D3");
        });

        modelBuilder.Entity<LeaveRequest>(entity =>
        {
            entity.HasKey(e => e.LeaveRequestId).HasName("PK__LeaveReq__609421EEC90C63B8");

            entity.Property(e => e.Reason).HasMaxLength(300);
            entity.Property(e => e.Status).HasMaxLength(30);

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("FK__LeaveRequ__Appro__52593CB8");

            entity.HasOne(d => d.Internee).WithMany(p => p.LeaveRequests)
                .HasForeignKey(d => d.InterneeId)
                .HasConstraintName("FK__LeaveRequ__Inter__5165187F");
        });

        modelBuilder.Entity<PerformanceEvaluation>(entity =>
        {
            entity.HasKey(e => e.EvaluationId).HasName("PK__Performa__36AE68F38BA6AD6D");

            entity.Property(e => e.Grade).HasMaxLength(20);

            entity.HasOne(d => d.Internee).WithMany(p => p.PerformanceEvaluations)
                .HasForeignKey(d => d.InterneeId)
                .HasConstraintName("FK__Performan__Inter__5812160E");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A228A6FAF");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B616009099E1A").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.HasKey(e => e.UniversityId).HasName("PK__Universi__9F19E1BC80ABC63C");

            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.UniversityName).HasMaxLength(200);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C34D055B9");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E40E96BC8B").IsUnique();

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Designation).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.MobileNo).HasMaxLength(20);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__3C69FB99");
        });

        modelBuilder.Entity<WeeklyProgress>(entity =>
        {
            entity.HasKey(e => e.ProgressId).HasName("PK__WeeklyPr__BAE29CA5262948DA");

            entity.ToTable("WeeklyProgress");

            entity.HasOne(d => d.Internee).WithMany(p => p.WeeklyProgresses)
                .HasForeignKey(d => d.InterneeId)
                .HasConstraintName("FK__WeeklyPro__Inter__5535A963");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
