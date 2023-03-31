using Tasks.Domain.Models.Tasks;
using Task = Tasks.Domain.Models.Tasks.Task;

namespace Tasks.DAL.Repositories.Interface
{
    public interface ITaskRepository
    {
        List<Task> GetAllTasks();
        int AddTask(TasksChangeModel taskContractorInitiator);
        Task GetTaskById(int id);
        void UpdateTask(TasksChangeModel tasksChangeModel);
    }
}
