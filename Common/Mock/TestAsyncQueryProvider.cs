using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Nettolicious.Common.Mock {
  public class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider {
    private readonly IQueryProvider _inner;

    internal TestAsyncQueryProvider(IQueryProvider inner) {
      _inner = inner;
    }

    public IQueryable CreateQuery(Expression expression) {
      return new TestAsyncEnumerable<TEntity>(expression);
    }

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression) {
      return new TestAsyncEnumerable<TElement>(expression);
    }

    public object Execute(Expression expression) {
      return _inner.Execute(expression);
    }

    public TResult Execute<TResult>(Expression expression) {
      return _inner.Execute<TResult>(expression);
    }

    public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression) {
      return new TestAsyncEnumerable<TResult>(expression);
    }

    //
    // Approach from MockQueryable by romantitov https://github.com/romantitov
    // (https://github.com/romantitov/MockQueryable/blob/master/src/MockQueryable/MockQueryable/TestAsyncEnumerable.cs#L59-L73)
    //
    // (Thanks https://github.com/SuricateCan for your suggestion!)
    //
    public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken) {
      var expectedResultType = typeof(TResult).GetGenericArguments()[0];
      var executionResult = typeof(IQueryProvider)
          .GetMethod(
              name: nameof(IQueryProvider.Execute),
              genericParameterCount: 1,
              types: new[] { typeof(Expression) })
          .MakeGenericMethod(expectedResultType)
          .Invoke(this, new[] { expression });

      return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))
          .MakeGenericMethod(expectedResultType)
          .Invoke(null, new[] { executionResult });
    }
  }
}
