using System;
using System.Collections.Generic;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcClient.Models
{
    public class TaskViewModel
    {
        public IList<ToDoTask> Tasks { get; set; }
        public ToDoTask TaskDetail { get; set; }
        public SelectList UserNotJointed { get; set; }
        public JointUser jointUser { get; set; }
        public TaskViewModel()
        {

        }
        public TaskViewModel(IList<ToDoTask> tasks)
        {
            Tasks = tasks;
        }
        // public IList<User> Users { get;set;}

    }
}
