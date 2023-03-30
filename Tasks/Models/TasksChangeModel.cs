using T = Tasks.Domain.Models.Tasks;

namespace Tasks.Models
{
    public class TasksChangeModel
    {
        public T.Task Task { get; set; }
        public List<int> UserIds { get; set; }
    }
}
