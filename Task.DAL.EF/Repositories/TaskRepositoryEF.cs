using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Tasks.DAL.Repositories.Interface;
using Tasks.Domain.Models;
using T = Tasks.Domain.Models.Tasks;
using Task.DAL.EF;

public class TaskRepositoryEF : ITaskRepository
{
    private readonly PostgreeContext _dbContext;
    
    public TaskRepositoryEF(PostgreeContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<T.Task> GetAllTasks()
    {
        return _dbContext.Tasks.ToList();
    }

    public T.Task GetTaskById(int id)
    {
        return _dbContext.Tasks.FirstOrDefault(t => t.Id == id);
    }

    public T.Task AddTask(T.Task task)
    {
        task.ExpirationDate = DateTime.SpecifyKind(task.ExpirationDate, DateTimeKind.Utc);
        task.CreatedDate = DateTime.SpecifyKind(task.CreatedDate, DateTimeKind.Utc);
        _dbContext.Tasks.Add(task);
        _dbContext.SaveChanges();
        return task;
    }

    public void UpdateTask(T.Task task)
    {
        task.ExpirationDate = DateTime.SpecifyKind(task.ExpirationDate, DateTimeKind.Utc);
        task.CreatedDate = DateTime.SpecifyKind(task.CreatedDate, DateTimeKind.Utc);
        _dbContext.Tasks.Update(task);
        _dbContext.SaveChanges();
    }

    public void DeleteTask(int id)
    {
        var taskToDelete = _dbContext.Tasks.FirstOrDefault(t => t.Id == id);
        if (taskToDelete != null)
        {
            _dbContext.Tasks.Remove(taskToDelete);
            _dbContext.SaveChanges();
        }
            
    }
    
}