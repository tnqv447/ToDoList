using System;
using System.Collections.Generic;
using AppCore.Models;

namespace AppCore.Interfaces
{
    public interface IToDoTaskRepos : IRepository<ToDoTask>
    {
        IList<ToDoTask> GetTasksForUser(int userId);
        IList<int> GetListId();
        IList<string> GetListTitle();
        void SetOnProgress(User source, ToDoTask task);
        void SetDone(User source, ToDoTask task);
        void SetNew(User source, ToDoTask task);
        void SetPublic(User source, ToDoTask task);
        void SetPrivate(User source, ToDoTask task);
        Boolean CheckUserInTask(int userId, ToDoTask task);
    }
}