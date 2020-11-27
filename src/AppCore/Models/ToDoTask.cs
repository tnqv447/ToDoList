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
        public virtual IList<AttachedFile> AttachedFile { get; set; }
    }
}