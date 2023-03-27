using Tasks.Models;

namespace Tasks.DAL.Repositories.Interface
{
    public interface ITaskRepository
    {
        List<Models.Task> GetAllTasks();
        Models.Task GetTaskById(int id);
        Models.Task AddTask(Models.Task task);
        void UpdateTask(Models.Task task);
        void DeleteTask(int id);
    }
}
