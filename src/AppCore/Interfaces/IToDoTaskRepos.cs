using AppCore.Models;

namespace AppCore.Interfaces
{
    public interface IToDoTaskRepos : IRepository<ToDoTask>
    {
        void SetOnProgress(User source, ToDoTask task);
        void SetDone(User source, ToDoTask task);
    }
}