using System;
using System.Collections.Generic;
using System.Linq;
using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Repositories
{
    public class ToDoTaskRepos : Repository<ToDoTask>, IToDoTaskRepos
    {
        private readonly ToDoListContext _context;
        public ToDoTaskRepos(ToDoListContext context) : base(context)
        {
            _context = context;
        }
        public IList<ToDoTask> GetTasksForUser(int userId){
            var tasks = this.GetAll();
            return tasks?.Where(m => m.RegisteredUserId.Equals(userId) || m.Scope.Equals(SCOPE.PUBLIC)).ToList() ?? null;
        }

        public void SetOnProgress(User source, ToDoTask task){
            this.UpdateStatus(source, task, STATUS.ON_PROGRESS);
        }
        public void SetDone(User source, ToDoTask task){
            this.UpdateStatus(source, task, STATUS.DONE);
        }

        private void UpdateStatus(User source, ToDoTask task, STATUS status){
            task.Status = status;
            this.Update(null, task);

            var log = new DbLog(DateTime.Now, ACTION_TARGET.TASK, ACTION.CHANGE_STATUS, source.Id, task.Id, task.Title, EnumConverter.Convert(status), null);
            _context.DbLogs.Add(log);
            _context.SaveChanges();
        }

        public void SetPublic(User source, ToDoTask task){
            this.UpdateScope(source, task, SCOPE.PUBLIC);
        }
        public void SetPrivate(User source, ToDoTask task){
            this.UpdateScope(source, task, SCOPE.PRIVATE);
        }
        private void UpdateScope(User source, ToDoTask task, SCOPE scope){
            task.Scope = scope;
            this.Update(null, task);

            var log = new DbLog(DateTime.Now, ACTION_TARGET.TASK, ACTION.CHANGE_SCOPE, source.Id, task.Id, task.Title, null, EnumConverter.Convert(scope));
            _context.DbLogs.Add(log);
            _context.SaveChanges();
        }
        
    }
}