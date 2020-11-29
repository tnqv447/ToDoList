using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCore.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [Display(Name = "Start date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd/MM/yyyy}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }

        public int RegisteredUserId { get; set; }
        public virtual JointUser RegisteredUser { get; set; }

        public STATUS Status { get; set; }
        public SCOPE Scope { get; set; }

        public virtual IList<JointUser> JointUsers { get; set; }
        public virtual IList<Comment> Comments { get; set; }
        public virtual IList<AttachedFile> AttachedFiles { get; set; }

        [NotMapped]
        public string StatusName { get { return EnumConverter.Convert(this.Status); } } 
        
        [NotMapped]
        public string ScopeName { get { return EnumConverter.Convert(this.Scope); } }

        public ToDoTask() { }

        public ToDoTask(string title, string description, DateTime startDate, DateTime endDate, int Registered_userId, STATUS status, SCOPE scope)
        {
            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            RegisteredUserId = Registered_userId;
            Status = status;
            Scope = scope;
        }
        public ToDoTask(ToDoTask task) { this.Copy(task);  }

        public void Copy(ToDoTask task){
            Title = task.Title;
            Description = task.Description;
            StartDate = task.StartDate;
            EndDate = task.EndDate;
            RegisteredUserId = task.RegisteredUserId;
            Status = task.Status;
            Scope = task.Scope;
        }
    }
}