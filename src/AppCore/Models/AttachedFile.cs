namespace AppCore.Models
{
    public class AttachedFile
    {
        public int Id { get; set; }

        public int ToDoTaskId { get; set; }
        public virtual ToDoTask ToDoTask { get; set; }

        public string FileUrl { get; set; }
        public AttachedFile()
        {
        }
        public AttachedFile(AttachedFile attachedFile)
        {
            this.Copy(attachedFile);
        }

        public AttachedFile(int toDoTaskId, string fileUrl)
        {
            this.ToDoTaskId = toDoTaskId;
            this.FileUrl = fileUrl;

        }

        public void Copy(AttachedFile attachedFile){
            
            this.ToDoTaskId = attachedFile.ToDoTaskId;
            this.FileUrl = attachedFile.FileUrl;
        }
        

        
    }
}