using AppCore.Models;

namespace AppCore.Interfaces
{
    public interface IJointUserRepos : IRepository<JointUser>
    {
        JointUser GetByUserAndTask(int usersId, int taskId);
    }
}