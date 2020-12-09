using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcClient.Models
{
    public class TaskViewModel
    {
        // public IList<ToDoTask> Tasks { get; set; }
        public PaginatedList<ToDoTask> Tasks { get; set; }
        public ToDoTask TaskDetail { get; set; }
        public SelectList UserNotJointed { get; set; }
        public SelectList Users { get; set; }
        public JointUser jointUser { get; set; }
        public string Name { get; set; }
        public bool isInTask { get; set; }
        public TaskViewModel()
        {

        }
        public TaskViewModel(PaginatedList<ToDoTask> tasks)
        {
            Tasks = tasks;
        }
        // public IList<User> Users { get;set;}

    }

    public enum Scope
    {
        [Display(Name = "Public")]
        PUBLIC,
        [Display(Name = "Private")]
        PRIVATE
    }
    public enum Status
    {
        [Display(Name = "Mới")]
        NEW,
        [Display(Name = "Đang tiến hành")]
        ON_PROGRESS,
        [Display(Name = "Đã hoàn thành")]
        DONE
    }
}
