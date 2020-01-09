using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

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
