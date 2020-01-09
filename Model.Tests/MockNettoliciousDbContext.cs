using Microsoft.EntityFrameworkCore;
using Moq;
using Nettolicious.Model.SpResults;
using System;
using System.Linq;

namespace Nettolicious.Model.Tests {
  public class MockNettoliciousDbContext : NettoliciousDbContext {

    public Mock<NettoliciousDbContext> MockDbContext { get; private set; }

    public MockNettoliciousDbContext(DbContextOptions<NettoliciousDbContext> options) : base(options) {
      var optionsBuilder = new DbContextOptionsBuilder<NettoliciousDbContext>();
      optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
      MockDbContext = new Mock<NettoliciousDbContext>(options);
    }

    public static MockNettoliciousDbContext CreateDbContext() {
      var optionsBuilder = new DbContextOptionsBuilder<NettoliciousDbContext>();
      optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
      return new MockNettoliciousDbContext(optionsBuilder.Options);
    }

    public override IQueryable<EmployeeManager> ExecGetEmployeeManagers(int businessEntityId) {
      return MockDbContext.Object.ExecGetEmployeeManagers(businessEntityId);
    }
  }
}
