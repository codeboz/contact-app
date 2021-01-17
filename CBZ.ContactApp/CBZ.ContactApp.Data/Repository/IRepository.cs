using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace CBZ.ContactApp.Data.Repository
{
    
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        Task<T> Find([NotNull]params object[] keyValues);
        IQueryable<T> Where([NotNull] Func<T, bool> predicate);
        Task<T>  Add([NotNull]T t);
        Task<T> Update([NotNull]T t);
        Task<T> Remove([NotNull]T t);
    }
}
