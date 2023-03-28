using T = Tasks.Domain.Models.Tasks;

namespace Tasks.Models
{
    public class TasksModel
    {
        public List<T.Task> Tasks { get; set; }
        public int PageCurrent { get; set; }
        public int PageCount { get; set; }
    }
}
