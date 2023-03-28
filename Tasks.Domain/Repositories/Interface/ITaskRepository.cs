using Task = Tasks.Domain.Models.Tasks.Task;

namespace Tasks.DAL.Repositories.Interface
{
    public interface ITaskRepository
    {
        List<Task> GetAllTasks();
        Task GetTaskById(int id);
        Task AddTask(Task task);
        void UpdateTask(Task task);
        void DeleteTask(int id);
    }
}
