using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace Nettolicious.Model {
  public class Configuration : Module {
    private readonly string dbConnString;

    public Configuration(string dbConnString) {
      dbConnString = dbConnString ?? throw new ArgumentNullException(nameof(dbConnString));
    }

    protected override void Load(ContainerBuilder builder) {
      builder.Register(c => NettoliciousDbContext.NewNettoliciousDbContext(dbConnString))
        .As<NettoliciousDbContext>()
        .InstancePerLifetimeScope();
    }
  }
}
