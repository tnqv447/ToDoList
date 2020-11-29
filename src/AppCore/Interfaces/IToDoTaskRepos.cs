using System.Collections.Generic;
using AppCore.Models;

namespace AppCore.Interfaces
{
    public interface IToDoTaskRepos : IRepository<ToDoTask>
    {
        IList<ToDoTask> GetTasksForUser(int userId);
        void SetOnProgress(User source, ToDoTask task);
        void SetDone(User source, ToDoTask task);

        void SetPublic(User source, ToDoTask task);
        void SetPrivate(User source, ToDoTask task);
    }
}