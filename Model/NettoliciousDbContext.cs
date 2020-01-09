using Microsoft.EntityFrameworkCore;
using Nettolicious.Model.Entities;

namespace Nettolicious.Model {
  public partial class NettoliciousDbContext : DbContext {
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }
    public virtual DbSet<EmployeeDepartmentHistory> EmployeeDepartmentHistories { get; set; }
    public virtual DbSet<EmployeePayHistory> EmployeePayHistories { get; set; }
    public virtual DbSet<JobCandidate> JobCandidates { get; set; }
    public virtual DbSet<Shift> Shifts { get; set; }

    public NettoliciousDbContext(DbContextOptions<NettoliciousDbContext> options) : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
      modelBuilder.Entity<Department>(entity => {
        entity.HasComment("Lookup table containing the departments within the Adventure Works Cycles company.");

        entity.HasIndex(e => e.Name)
            .HasName("AK_Department_Name")
            .IsUnique();

        entity.Property(e => e.DepartmentId).HasComment("Primary key for Department records.");

        entity.Property(e => e.GroupName).HasComment("Name of the group to which the department belongs.");

        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.");

        entity.Property(e => e.Name).HasComment("Name of the department.");
      });

      modelBuilder.Entity<Employee>(entity => {
        entity.HasKey(e => e.BusinessEntityId)
            .HasName("PK_Employee_BusinessEntityID");

        entity.HasComment("Employee information such as salary, department, and title.");

        entity.HasIndex(e => e.LoginId)
            .HasName("AK_Employee_LoginID")
            .IsUnique();

        entity.HasIndex(e => e.NationalIdnumber)
            .HasName("AK_Employee_NationalIDNumber")
            .IsUnique();

        entity.HasIndex(e => e.Rowguid)
            .HasName("AK_Employee_rowguid")
            .IsUnique();

        entity.Property(e => e.BusinessEntityId)
            .HasComment("Primary key for Employee records.  Foreign key to BusinessEntity.BusinessEntityID.")
            .ValueGeneratedNever();

        entity.Property(e => e.BirthDate).HasComment("Date of birth.");

        entity.Property(e => e.CurrentFlag)
            .HasDefaultValueSql("((1))")
            .HasComment("0 = Inactive, 1 = Active");

        entity.Property(e => e.Gender)
            .IsFixedLength()
            .HasComment("M = Male, F = Female");

        entity.Property(e => e.HireDate).HasComment("Employee hired on this date.");

        entity.Property(e => e.JobTitle).HasComment("Work title such as Buyer or Sales Representative.");

        entity.Property(e => e.LoginId).HasComment("Network login.");

        entity.Property(e => e.MaritalStatus)
            .IsFixedLength()
            .HasComment("M = Married, S = Single");

        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.");

        entity.Property(e => e.NationalIdnumber).HasComment("Unique national identification number such as a social security number.");

        entity.Property(e => e.OrganizationLevel)
            .HasComment("The depth of the employee in the corporate hierarchy.")
            .HasComputedColumnSql("([OrganizationNode].[GetLevel]())");

        entity.Property(e => e.Rowguid)
            .HasDefaultValueSql("(newid())")
            .HasComment("ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.");

        entity.Property(e => e.SalariedFlag)
            .HasDefaultValueSql("((1))")
            .HasComment("Job classification. 0 = Hourly, not exempt from collective bargaining. 1 = Salaried, exempt from collective bargaining.");

        entity.Property(e => e.SickLeaveHours).HasComment("Number of available sick leave hours.");

        entity.Property(e => e.VacationHours).HasComment("Number of available vacation hours.");
      });

      modelBuilder.Entity<EmployeeDepartmentHistory>(entity => {
        entity.HasKey(e => new { e.BusinessEntityId, e.StartDate, e.DepartmentId, e.ShiftId })
            .HasName("PK_EmployeeDepartmentHistory_BusinessEntityID_StartDate_DepartmentID");

        entity.HasComment("Employee department transfers.");

        entity.HasIndex(e => e.DepartmentId);

        entity.HasIndex(e => e.ShiftId);

        entity.Property(e => e.BusinessEntityId).HasComment("Employee identification number. Foreign key to Employee.BusinessEntityID.");

        entity.Property(e => e.StartDate).HasComment("Date the employee started work in the department.");

        entity.Property(e => e.DepartmentId).HasComment("Department in which the employee worked including currently. Foreign key to Department.DepartmentID.");

        entity.Property(e => e.ShiftId).HasComment("Identifies which 8-hour shift the employee works. Foreign key to Shift.Shift.ID.");

        entity.Property(e => e.EndDate).HasComment("Date the employee left the department. NULL = Current department.");

        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.");

        entity.HasOne(d => d.BusinessEntity)
            .WithMany(p => p.EmployeeDepartmentHistories)
            .HasForeignKey(d => d.BusinessEntityId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Department)
            .WithMany(p => p.EmployeeDepartmentHistories)
            .HasForeignKey(d => d.DepartmentId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        entity.HasOne(d => d.Shift)
            .WithMany(p => p.EmployeeDepartmentHistories)
            .HasForeignKey(d => d.ShiftId)
            .OnDelete(DeleteBehavior.ClientSetNull);
      });

      modelBuilder.Entity<EmployeePayHistory>(entity => {
        entity.HasKey(e => new { e.BusinessEntityId, e.RateChangeDate })
            .HasName("PK_EmployeePayHistory_BusinessEntityID_RateChangeDate");

        entity.HasComment("Employee pay history.");

        entity.Property(e => e.BusinessEntityId).HasComment("Employee identification number. Foreign key to Employee.BusinessEntityID.");

        entity.Property(e => e.RateChangeDate).HasComment("Date the change in pay is effective");

        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.");

        entity.Property(e => e.PayFrequency).HasComment("1 = Salary received monthly, 2 = Salary received biweekly");

        entity.Property(e => e.Rate).HasComment("Salary hourly rate.");

        entity.HasOne(d => d.BusinessEntity)
            .WithMany(p => p.EmployeePayHistories)
            .HasForeignKey(d => d.BusinessEntityId)
            .OnDelete(DeleteBehavior.ClientSetNull);
      });

      modelBuilder.Entity<JobCandidate>(entity => {
        entity.HasComment("Résumés submitted to Human Resources by job applicants.");

        entity.HasIndex(e => e.BusinessEntityId);

        entity.Property(e => e.JobCandidateId).HasComment("Primary key for JobCandidate records.");

        entity.Property(e => e.BusinessEntityId).HasComment("Employee identification number if applicant was hired. Foreign key to Employee.BusinessEntityID.");

        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.");

        entity.Property(e => e.Resume).HasComment("Résumé in XML format.");
      });

      modelBuilder.Entity<Shift>(entity => {
        entity.HasComment("Work shift lookup table.");

        entity.HasIndex(e => e.Name)
            .HasName("AK_Shift_Name")
            .IsUnique();

        entity.HasIndex(e => new { e.StartTime, e.EndTime })
            .HasName("AK_Shift_StartTime_EndTime")
            .IsUnique();

        entity.Property(e => e.ShiftId)
            .HasComment("Primary key for Shift records.")
            .ValueGeneratedOnAdd();

        entity.Property(e => e.EndTime).HasComment("Shift end time.");

        entity.Property(e => e.ModifiedDate)
            .HasDefaultValueSql("(getdate())")
            .HasComment("Date and time the record was last updated.");

        entity.Property(e => e.Name).HasComment("Shift description.");

        entity.Property(e => e.StartTime).HasComment("Shift start time.");
      });

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}
