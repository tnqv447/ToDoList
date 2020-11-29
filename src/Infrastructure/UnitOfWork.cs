using AppCore.Interfaces;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ToDoListContext _context;

        public UnitOfWork(ToDoListContext context)
        {
            _context = context;
            ToDoTasks = new ToDoTaskRepos(_context);
            Users = new UserRepos(_context);
            JointUsers = new JointUserRepos(_context);
            Comments = new CommentRepos(_context);
            AttachedFiles = new AttachedFileRepos(_context);
            DbLogs = new DbLogRepos(_context);

        }
        public IUserRepos Users { get; private set; }
        public IToDoTaskRepos ToDoTasks { get; private set;}
        public IJointUserRepos JointUsers { get; private set;}
        public ICommentRepos Comments { get; private set;}
        public IAttachedFileRepos AttachedFiles { get; private set;}
        public IDbLogRepos DbLogs { get; private set;}

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}