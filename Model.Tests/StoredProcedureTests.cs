using Nettolicious.Common.Extensions;
using Nettolicious.Common.Mock;
using Nettolicious.Model.SpResults;
using System.Linq;
using Xunit;

namespace Nettolicious.Model.Tests {
  public class StoredProcedureTests {

    [Fact]
    public void ExecGetEmployeeManagers_Should_ReturnEmployeeManagers() {
      // Arrange
      using var scope = DependencyResolver.Current.BeginLifetimeScope();
      var dbContext = scope.GetService<NettoliciousDbContext>() as MockNettoliciousDbContext;
      dbContext.MockDbContext
        .Setup(x => x.ExecGetEmployeeManagers(15))
        .Returns(new TestAsyncEnumerable<EmployeeManager>(MockData.GetEmployeeManagers(15)));
      int businessEntityId = 15;
      // Act
      var employeeManagers = dbContext.ExecGetEmployeeManagers(businessEntityId).ToList();
      // Assert
      Assert.NotNull(employeeManagers);
      Assert.Equal(2, employeeManagers.Count);
    }
  }
}
