using Tasks.Domain.Models.Tasks;
using Task = Tasks.Domain.Models.Tasks.Task;

namespace Tasks.DAL.Repositories.Interface
{
    public interface ITaskRepository
    {
        List<Task> GetAllTasks();
        Task AddTask(TasksChangeModel tasksChangeModel);
        Task GetTaskById(int id);
        void UpdateTask(TasksChangeModel tasksChangeModel);
        void DeleteTask(int id);
    }
}
