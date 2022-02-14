using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace TiendaServicios.Api.Libro.Test
{
    public class AsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public AsyncEnumerable(Expression expression) : base(expression)
        {
            
        }

        public AsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable)
        {

        }

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
        {
            return new AsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new AsyncQueryProvider<T>(this); }
        }
    }
}