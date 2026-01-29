using MatchArena.Application.Interfaces.Repositories.Generic;
using MatchArena.Domain.Entities;
using MatchArena.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Persistence.Implementations.Repositories.Generic
{
    internal class Repository<T>:IRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }


        public IQueryable<T> GetAll(
            Expression<Func<T,bool>>? func = null,
            Expression<Func<T,object>>? sort = null,
            bool isDesc = false,
            int page = 0,
            int take = 0,
            params string[]? includes  
            )
        {
            IQueryable<T> query = _dbSet;
            if (func is not null)
                query = query.Where(func);

            if(sort is not null)
            {
                if(isDesc)
                    query = query.OrderByDescending(sort);
                else
                    query = query.OrderBy(sort);
            }
            if (page>0 && take > 0)
            {
                query = query.Skip((page - 1) * take).Take(take);
            }

            if(includes is not null)
            {
                query = _getIncludes(query, includes);
            }

            return query;
        }

        public async Task<T?> GetByIdAsync(long id, params string[] includes)
        {
            IQueryable<T> query = _dbSet;
            if (includes is not null)
            {
                query = _getIncludes(query, includes);
            }

            return await query.FirstOrDefaultAsync(c => c.Id == id);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected IQueryable<T> _getIncludes(IQueryable<T> query,params string[]? includes)
        {
            for (int i = 0; i < includes.Length; i++)
            {
                query = query.Include(includes[i]);
            }
            return query;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> func)
        {
            return await _dbSet.AnyAsync(func);
        }

    }
}
