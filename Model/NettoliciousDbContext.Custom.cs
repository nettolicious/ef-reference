using Microsoft.EntityFrameworkCore;

namespace Nettolicious.Model {
  public partial class NettoliciousDbContext {

    public static NettoliciousDbContext NewNettoliciousDbContext(string connection) {
      var options = new DbContextOptionsBuilder<NettoliciousDbContext>();
      options.UseSqlServer(connection);
      return new NettoliciousDbContext(options.Options);
    }

    // The generated DbContext class is defined using the partial keyword
    // it contains a partial OnModelCreatingPartial method that is invoked at the end of the OnModelCreating method.
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder) {
      // Extend OnModelCreating here if necessary
      // RegisterViews(modelBuilder);
      // modelBuilder.Entity<SomeTypeNeedingAKey>().HasKey(c => new { c.Prop1, c.Prop2 });
    }
  }
}