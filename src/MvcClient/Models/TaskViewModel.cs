using System;
using System.Collections.Generic;
using AppCore.Models;

namespace MvcClient.Models
{
    public class TaskViewModel
    {
        public IList<ToDoTask> Tasks {  get; set;}

        public TaskViewModel(IList<ToDoTask> tasks)
        {
            Tasks = tasks;
        }
        // public IList<User> Users { get;set;}

    }   
}
