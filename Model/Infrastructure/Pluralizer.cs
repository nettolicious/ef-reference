using Microsoft.EntityFrameworkCore.Design;
using System.Globalization;

namespace Nettolicious.Model.Infrastructure {
  public class Pluralizer : IPluralizer {
    private readonly Inflector.Inflector _inflector;

    public Pluralizer() {
      _inflector = new Inflector.Inflector(CultureInfo.CurrentCulture);
      // This is so a table ending in Alias will not become the enity 'Alia'
      _inflector.CurrentCultureRules.Singulars.Add("(alias)$", "$1");
      // TPPS
      _inflector.CurrentCultureRules.Uncountables.Add("tpps");
    }

    public string Pluralize(string identifier) {
      return _inflector.Pluralize(identifier) ?? identifier;
    }

    public string Singularize(string identifier) {
      return _inflector.Singularize(identifier) ?? identifier;
    }
  }
}
