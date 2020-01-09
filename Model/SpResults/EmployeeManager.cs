using System.ComponentModel.DataAnnotations;

namespace Nettolicious.Model.SpResults {
  public class EmployeeManager {
    [Key]
    public int RecursionLevel { get; set; }
    public int BusinessEntityId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string OrganizationNode { get; set; }
    public string ManagerFirstName { get; set; }
    public string ManagerLastName { get; set; }
  }
}
