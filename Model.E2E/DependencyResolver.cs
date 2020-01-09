using Autofac;
using Microsoft.Extensions.Configuration;

namespace Nettolicious.Model.E2E {
  public static class DependencyResolver {
    private static IContainer mCurrent;

    public static IContainer Current {
      get {
        if (mCurrent == null) {
          var config = GetConfigurationRoot();
          var builder = new ContainerBuilder();
          builder.RegisterModule(new Model.Configuration(config["DbConnString"]));
          mCurrent = builder.Build();
        }
        return mCurrent;
      }
    }

    private static IConfiguration GetConfigurationRoot() {
      return new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
    }
  }
}
