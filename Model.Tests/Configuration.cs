using Autofac;
using Microsoft.EntityFrameworkCore;
using System;

namespace Nettolicious.Model.Tests {
  public class Configuration : Module {
    protected override void Load(ContainerBuilder builder) {
      // Model
      builder.Register(c => {
        var options = new DbContextOptionsBuilder<NettoliciousDbContext>();
        options.UseInMemoryDatabase(Guid.NewGuid().ToString());
        return new NettoliciousDbContext(options.Options);
      })
        .As<NettoliciousDbContext>()
        .InstancePerLifetimeScope();
    }
  }
}
