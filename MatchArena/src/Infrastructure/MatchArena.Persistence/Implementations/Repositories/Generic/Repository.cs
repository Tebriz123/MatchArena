using MatchArena.Application.Interfaces.Repositories.Generic;
using MatchArena.Domain.Entities;
using MatchArena.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
