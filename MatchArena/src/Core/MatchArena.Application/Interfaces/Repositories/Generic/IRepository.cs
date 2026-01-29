using MatchArena.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Interfaces.Repositories.Generic
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        public IQueryable<T> GetAll(
           Expression<Func<T, bool>>? func = null,
           Expression<Func<T, object>>? sort = null,
           bool isDesc = false,
           int page = 0,
           int take = 0,
           params string[] includes
           );
        public Task<T> GetByIdAsync(long id, params string[] includes);
        public void Add(T entity);
        public void Update(T entity);
        public void Remove(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> func);

        public Task SaveChangesAsync();
    }
}
