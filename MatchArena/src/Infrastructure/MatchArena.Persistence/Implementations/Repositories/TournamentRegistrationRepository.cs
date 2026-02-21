using MatchArena.Application.Interfaces.Repositories;
using MatchArena.Domain.Entities;
using MatchArena.Persistence.Contexts;
using MatchArena.Persistence.Implementations.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Persistence.Implementations.Repositories
{
    internal class TournamentRegistrationRepository:Repository<TournamentRegistration>,ITournamentRegistrationRepository
    {
        public TournamentRegistrationRepository(AppDbContext context):base(context) { }
        
    }
}
