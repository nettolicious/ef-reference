using Nettolicious.Common.Extensions;
using System.Linq;
using Xunit;

namespace Nettolicious.Model.E2E {
  public class StoredProcedureTests {

    [Fact]
    public void ExecGetEmployeeManagers_Should_ReturnEmployeeManagers() {
      // Arrange
      // TODO add the data for the test to the database here... currently depends on the data for 15 being there and count equaling 2
      using var scope = DependencyResolver.Current.BeginLifetimeScope();
      var dbContext = scope.GetService<NettoliciousDbContext>();
      int businessEntityId = 15;
      // Act
      var employeeManagers = dbContext.ExecGetEmployeeManagers(businessEntityId).ToList();
      // Assert
      Assert.NotNull(employeeManagers);
      Assert.Equal(2, employeeManagers.Count);
    }
  }
}
