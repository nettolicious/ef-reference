using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nettolicious.Model.Entities {
  [Table("Employee", Schema = "HumanResources")]
  public partial class Employee {
    public Employee() {
      EmployeeDepartmentHistories = new HashSet<EmployeeDepartmentHistory>();
      EmployeePayHistories = new HashSet<EmployeePayHistory>();
      JobCandidates = new HashSet<JobCandidate>();
    }

    [Key]
    [Column("BusinessEntityID")]
    public int BusinessEntityId { get; set; }
    [Required]
    [Column("NationalIDNumber")]
    [StringLength(15)]
    public string NationalIdnumber { get; set; }
    [Required]
    [Column("LoginID")]
    [StringLength(256)]
    public string LoginId { get; set; }
    public short? OrganizationLevel { get; set; }
    [Required]
    [StringLength(50)]
    public string JobTitle { get; set; }
    [Column(TypeName = "date")]
    public DateTime BirthDate { get; set; }
    [Required]
    [StringLength(1)]
    public string MaritalStatus { get; set; }
    [Required]
    [StringLength(1)]
    public string Gender { get; set; }
    [Column(TypeName = "date")]
    public DateTime HireDate { get; set; }
    [Required]
    public bool? SalariedFlag { get; set; }
    public short VacationHours { get; set; }
    public short SickLeaveHours { get; set; }
    [Required]
    public bool? CurrentFlag { get; set; }
    [Column("rowguid")]
    public Guid Rowguid { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime ModifiedDate { get; set; }

    [InverseProperty("BusinessEntity")]
    public virtual ICollection<EmployeeDepartmentHistory> EmployeeDepartmentHistories { get; set; }
    [InverseProperty("BusinessEntity")]
    public virtual ICollection<EmployeePayHistory> EmployeePayHistories { get; set; }
    [InverseProperty("BusinessEntity")]
    public virtual ICollection<JobCandidate> JobCandidates { get; set; }
  }
}
