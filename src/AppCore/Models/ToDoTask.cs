using System;
using System.Collections.Generic;

namespace AppCore.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public STATUS Status { get; set; }
        public SCOPE Scope { get; set; }

        public virtual IList<User> JointUsers { get; set; }
        public virtual IList<Comment> Comments { get; set; }
        public virtual IList<AttachedFile> AttachedFiles { get; set; }

        public ToDoTask() { }

        public ToDoTask(int id, string title, DateTime startDate, DateTime endDate, int userId, STATUS status, SCOPE scope)
        {
            Id = id;
            Title = title;
            StartDate = startDate;
            EndDate = endDate;
            UserId = userId;
            Status = status;
            Scope = scope;
        }
        public ToDoTask(ToDoTask task) { this.Copy(task);  }

        public void Copy(ToDoTask task){
            Id = task.Id;
            Title = task.Title;
            StartDate = task.StartDate;
            EndDate = task.EndDate;
            UserId = task.UserId;
            Status = task.Status;
            Scope = task.Scope;
        }
    }
}