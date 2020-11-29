using System;
namespace AppCore.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepos Users { get; }
        IToDoTaskRepos ToDoTasks { get; }
        IJointUserRepos JointUsers { get; }
        ICommentRepos Comments { get; }
        IAttachedFileRepos AttachedFiles { get; }
        IDbLogRepos DbLogs { get; }
        int Complete();
    }
}