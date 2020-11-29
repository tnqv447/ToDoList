using System;
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

        public void SetOnProgress(User source, ToDoTask task){
            this.UpdateStatus(source, task, STATUS.ON_PROGRESS);
        }
        public void SetDone(User source, ToDoTask task){
            this.UpdateStatus(source, task, STATUS.DONE);
        }

        private void UpdateStatus(User source, ToDoTask task, STATUS status){
            task.Status = status;
            this.Update(null, task);

            var log = new DbLog(DateTime.Now, ACTION_TARGET.TASK, ACTION.CHANGE_STATUS, source.Id, task.Id, task.Title, EnumConverter.Convert(status));
            _context.DbLogs.Add(log);
            _context.SaveChanges();
        }
        
    }
}