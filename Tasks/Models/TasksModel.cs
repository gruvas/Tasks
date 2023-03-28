using Task = Tasks.Domain.Models.Tasks.Task;

namespace Tasks.Models
{
    public class TasksModel
    {
        public List<Task> Tasks { get; set; }
        public int PageCurrent { get; set; }
        public int PageCount { get; set; }
    }
}
