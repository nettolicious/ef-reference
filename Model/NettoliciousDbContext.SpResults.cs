using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Nettolicious.Model.SpResults;
using System;
using System.Linq;

namespace Nettolicious.Model {
  public partial class NettoliciousDbContext {

    public virtual DbSet<EmployeeManager> EmployeeManagers {
      get => throw new NotImplementedException();
      set { }
    }

    public virtual IQueryable<EmployeeManager> ExecGetEmployeeManagers(int businessEntityId) {
      var param = new SqlParameter("BusinessEntityID", businessEntityId);
      return Set<EmployeeManager>().FromSqlRaw("exec [dbo].[uspGetEmployeeManagers] @BusinessEntityID", param);
    }
  }
}
