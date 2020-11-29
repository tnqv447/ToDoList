using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Repositories
{
    public class DbLogRepos : Repository<DbLog>, IDbLogRepos
    {
        private readonly ToDoListContext _context;
        public DbLogRepos(ToDoListContext context) : base(context)
        {
            _context = context;
        }
    }
}