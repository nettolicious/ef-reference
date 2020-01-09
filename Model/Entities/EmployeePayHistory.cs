using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nettolicious.Model.Entities {
  [Table("EmployeePayHistory", Schema = "HumanResources")]
  public partial class EmployeePayHistory {
    [Key]
    [Column("BusinessEntityID")]
    public int BusinessEntityId { get; set; }
    [Key]
    [Column(TypeName = "datetime")]
    public DateTime RateChangeDate { get; set; }
    [Column(TypeName = "money")]
    public decimal Rate { get; set; }
    public byte PayFrequency { get; set; }
    [Column(TypeName = "datetime")]
    public DateTime ModifiedDate { get; set; }

    [ForeignKey("BusinessEntityId")]
    [InverseProperty("EmployeePayHistories")]
    public virtual Employee BusinessEntity { get; set; }
  }
}
