using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Repositories
{
    public class AttachedFileRepos : Repository<AttachedFile>, IAttachedFileRepos
    {
        private readonly ToDoListContext _context;
        public AttachedFileRepos(ToDoListContext context) : base(context)
        {
            _context = context;
        }
    }
}