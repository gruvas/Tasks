using Task = Tasks.Domain.Models.Tasks.Task;

namespace Tasks.DAL.Repositories.Interface
{
    public interface ITaskRepository
    {
        List<Task> GetAllTasks();
        Task AddTask(Task task);
        Task GetTaskById(int id);
        List<int> GettingIdsTask();
        void UpdateTask(Task task);
        void DeleteTask(int id);
    }
}
