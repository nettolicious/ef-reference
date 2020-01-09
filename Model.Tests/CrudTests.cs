using Nettolicious.Common.Extensions;
using Nettolicious.Model.Entities;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Nettolicious.Model.Tests {
  public class CrudTests {
    [Fact]
    public async Task Add_Should_SaveNewDepartment() {
      // Arrange
      using var scope = DependencyResolver.Current.BeginLifetimeScope();
      var dbContext = scope.GetService<NettoliciousDbContext>();
      var department = new Department {
        Name = Guid.NewGuid().ToString(),
        GroupName = Guid.NewGuid().ToString()
      };
      // Act
      var tracking = dbContext.Departments.Add(department);
      await dbContext.SaveChangesAsync();
      // Assert
      Assert.NotNull(tracking);
      Assert.NotNull(tracking.Entity);
      Assert.Equal(department.Name, tracking.Entity.Name);
      Assert.Equal(department.GroupName, tracking.Entity.GroupName);
    }
  }
}
