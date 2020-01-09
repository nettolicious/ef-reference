using Autofac;

namespace Nettolicious.Model.Tests {
  public static class DependencyResolver {
    private static IContainer mCurrent;

    public static IContainer Current {
      get {
        if (mCurrent == null) {
          var builder = new ContainerBuilder();
          builder.RegisterModule(new Configuration());
          mCurrent = builder.Build();
        }
        return mCurrent;
      }
    }
  }
}
