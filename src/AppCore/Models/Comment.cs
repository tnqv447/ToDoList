using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCore.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int ToDoTaskId { get; set; }
        public virtual ToDoTask ToDoTask { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public DateTime PostDate { get; set; }
        public string Content { get; set; }

        [NotMapped]
        public string UserPostName { get { return User?.Name ?? ""; } } 

        public Comment() {}
        public Comment(Comment comment)
        {
            this.Copy(comment);
        }

        public Comment(int toDoTaskId, int userId, DateTime postDate, string content)
        {
            ToDoTaskId = toDoTaskId;
            UserId = userId;
            PostDate = postDate;
            Content = content;
        }

        public void Copy(Comment com){
            ToDoTaskId = com.ToDoTaskId;
            UserId = com.UserId;
            PostDate = com.PostDate;
            Content = com.Content;
        }
    }
}