using Autofac;

namespace Nettolicious.Model.Tests {
  public class Configuration : Module {
    protected override void Load(ContainerBuilder builder) {
      // Model
      builder.Register(c => MockNettoliciousDbContext.CreateDbContext())
        .As<NettoliciousDbContext>()
        .InstancePerLifetimeScope();
    }
  }
}
