namespace AppCore.Services
{
    public class AnalyzeUserTasks
    {
        public AnalyzeUserTasks()
        {
        }
        public int UserId { get; set; } = 0;
        public int TasksCount { get; set; } = 0;
        public int TaskDoneCount { get; set; } = 0;
        public int TaskNewCount { get; set; } = 0;
        public int TaskOnProgressCount { get; set; } = 0;
        public int TaskDelayedCount { get; set; } = 0;

        public AnalyzeUserTasks(int userId, int tasksCount, int taskDoneCount, int taskNewCount, int taskOnProgressCount, int taskDelayedCount)
        {
            UserId = userId;
            TasksCount = tasksCount;
            TaskDoneCount = taskDoneCount;
            TaskNewCount = taskNewCount;
            TaskOnProgressCount = taskOnProgressCount;
            TaskDelayedCount = taskDelayedCount;
        }
    }
}