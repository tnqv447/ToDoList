using System;
using System.Collections.Generic;
using AppCore.Models;
using AppCore.Services;

namespace MvcClient.Models
{
    public class StatisticModel
    {
        public AnalyzeUserTasks Analyze { get; set; }
        public IList<User> Users { get; set; }
        public IList<ToDoTask> Tasks { get; set; }
        public int id { get; set; }
        public string UserName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}