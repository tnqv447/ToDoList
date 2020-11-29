using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Repositories
{
    public class CommentRepos : Repository<Comment>, ICommentRepos
    {
        private readonly ToDoListContext _context;
        public CommentRepos(ToDoListContext context) : base(context)
        {
            _context = context;
        }
    }
}