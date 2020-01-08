using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nettolicious.Model.Infrastructure {
  public class ScaffoldingDesignTimeServices : IDesignTimeServices {
    public void ConfigureDesignTimeServices(IServiceCollection serviceCollection) {
      // Code templates
      var options = ReverseEngineerOptions.DbContextAndEntities;
      serviceCollection.AddHandlebarsScaffolding(options);
      // Pluralization
      serviceCollection.AddSingleton<IPluralizer, Pluralizer>();
    }
  }
}
