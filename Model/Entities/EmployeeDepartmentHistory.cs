using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nettolicious.Model.Entities {
  [Table("EmployeeDepartmentHistory", Schema = "HumanResources")]
  public partial class EmployeeDepartmentHistory {
    [Key]
    [Column("BusinessEntityID")]
    public int BusinessEntityId { get; set; }
    [Key]
    [Column("DepartmentID")]
    public short DepartmentId { get; set; }
    [Key]
    [Column("ShiftID")]
    public byte ShiftId { get; set; }
    [Key]
    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }
    [Column(TypeName = "date")]
    public DateTime? EndDate { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime ModifiedDate { get; set; }

    [ForeignKey("BusinessEntityId")]
    [InverseProperty("EmployeeDepartmentHistories")]
    public virtual Employee BusinessEntity { get; set; }
    [ForeignKey("DepartmentId")]
    [InverseProperty("EmployeeDepartmentHistories")]
    public virtual Department Department { get; set; }
    [ForeignKey("ShiftId")]
    [InverseProperty("EmployeeDepartmentHistories")]
    public virtual Shift Shift { get; set; }
  }
}
