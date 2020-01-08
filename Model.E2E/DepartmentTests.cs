using System;
using System.Threading.Tasks;
using Xunit;

namespace Nettolicious.Model.E2E {
  public class DepartmentTests {
    [Fact]
    public async Task Add_Should_SaveNewDepartment() {
      // Arrange
      using var scope = DependencyResolver.Current.BeginLifetimeScope();

      // Act

      // Assert

    }
  }
}
