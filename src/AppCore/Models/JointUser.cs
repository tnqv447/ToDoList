using System.ComponentModel.DataAnnotations.Schema;

namespace AppCore.Models
{
    public class JointUser
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User{ get; set; }
        
        public int ToDoTaskId { get; set; }
        public virtual ToDoTask ToDoTask { get; set; }

        [NotMapped]
        public string UserName { get { return this.User?.Name ?? ""; } } 
        [NotMapped]
        public string TaskTitle { get { return this.ToDoTask?.Title ?? ""; } } 

        public JointUser()
        {
            
        }
        public JointUser(JointUser joint)
        {
            this.Copy(joint);
        }
        public JointUser(int userId, int toDoTaskId)
        {
            UserId = userId;
            ToDoTaskId = toDoTaskId;
        }
        public void Copy(JointUser joint)
        {
            UserId = joint.UserId;
            ToDoTaskId = joint.ToDoTaskId;
        }
    }
}