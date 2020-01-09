using Nettolicious.Common.Extensions;
using Nettolicious.Model.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Nettolicious.Model.E2E {
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

    [Fact]
    public async Task Single_Should_GetDepartment() {
      // Arrange
      using var scope = DependencyResolver.Current.BeginLifetimeScope();
      var dbContext = scope.GetService<NettoliciousDbContext>();
      var department = new Department {
        Name = Guid.NewGuid().ToString(),
        GroupName = Guid.NewGuid().ToString()
      };
      var tracking = dbContext.Departments.Add(department);
      await dbContext.SaveChangesAsync();
      // Act
      var getResult = dbContext.Departments.Single(x => x.DepartmentId == tracking.Entity.DepartmentId);
      // Assert
      Assert.NotNull(getResult);
      Assert.Equal(department.Name, getResult.Name);
      Assert.Equal(department.GroupName, getResult.GroupName);
    }

    [Fact]
    public async Task Update_Should_UpdateDepartment() {
      // Arrange
      using var scope = DependencyResolver.Current.BeginLifetimeScope();
      var dbContext = scope.GetService<NettoliciousDbContext>();
      var department = new Department {
        Name = Guid.NewGuid().ToString(),
        GroupName = Guid.NewGuid().ToString()
      };
      var tracking = dbContext.Departments.Add(department);
      await dbContext.SaveChangesAsync();
      var toUpdate = dbContext.Departments.Single(x => x.DepartmentId == tracking.Entity.DepartmentId);
      var newName = Guid.NewGuid().ToString();
      // Act
      toUpdate.Name = newName;
      await dbContext.SaveChangesAsync();
      // Assert
      var updated = dbContext.Departments.Single(x => x.DepartmentId == tracking.Entity.DepartmentId);
      Assert.NotNull(updated);
      Assert.Equal(newName, updated.Name);
    }

    [Fact]
    public async Task Delete_Should_DeleteDepartment() {
      // Arrange
      using var scope = DependencyResolver.Current.BeginLifetimeScope();
      var dbContext = scope.GetService<NettoliciousDbContext>();
      var department = new Department {
        Name = Guid.NewGuid().ToString(),
        GroupName = Guid.NewGuid().ToString()
      };
      var tracking = dbContext.Departments.Add(department);
      await dbContext.SaveChangesAsync();
      // Act
      dbContext.Departments.Remove(tracking.Entity);
      await dbContext.SaveChangesAsync();
      // Assert
      var deleted = dbContext.Departments.SingleOrDefault(x => x.DepartmentId == tracking.Entity.DepartmentId);
      Assert.Null(deleted);
    }
  }
}
