using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Repositories
{
    public class JointUserRepos : Repository<JointUser>, IJointUserRepos
    {
        private readonly ToDoListContext _context;
        public JointUserRepos(ToDoListContext context) : base(context)
        {
            _context = context;
        }
    }
}