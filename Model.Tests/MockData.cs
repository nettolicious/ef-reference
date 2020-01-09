using Nettolicious.Model.SpResults;
using System.Collections.Generic;

namespace Nettolicious.Model.Tests {
  public static class MockData {
    public static List<EmployeeManager> GetEmployeeManagers(int businessEntityId) {
      return new List<EmployeeManager> {
        new EmployeeManager {
          BusinessEntityId = businessEntityId
        },
        new EmployeeManager {
          BusinessEntityId = businessEntityId
        }
      };
    }
  }
}
