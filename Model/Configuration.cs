using Autofac;
using System;

namespace Nettolicious.Model {
  public class Configuration : Module {
    private readonly string mDbConnString;

    public Configuration(string dbConnString) {
      mDbConnString = dbConnString ?? throw new ArgumentNullException(nameof(dbConnString));
    }

    protected override void Load(ContainerBuilder builder) {
      builder.Register(c => NettoliciousDbContext.NewNettoliciousDbContext(mDbConnString))
        .As<NettoliciousDbContext>()
        .InstancePerLifetimeScope();
    }
  }
}
