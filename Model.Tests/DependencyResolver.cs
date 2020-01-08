using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace Nettolicious.Model.Tests {
  public static class DependencyResolver {
    private static IContainer current;

    public static IContainer Current {
      get {
        if (current == null) {
          var builder = new ContainerBuilder();
          builder.RegisterModule(new Configuration());
          current = builder.Build();
        }
        return current;
      }
    }
  }
}
