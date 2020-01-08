using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace Nettolicious.Model.E2E {
  public static class DependencyResolver {
    private static IContainer current;

    public static IContainer Current {
      get {
        if (current == null) {
          var config = GetConfigurationRoot();
          var builder = new ContainerBuilder();
          builder.RegisterModule(new Model.Configuration(config["DbConnString"]));
          current = builder.Build();
        }
        return current;
      }
    }

    private static IConfiguration GetConfigurationRoot() {
      return new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
    }
  }
}
