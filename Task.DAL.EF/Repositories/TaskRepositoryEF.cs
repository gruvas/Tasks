using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Tasks.DAL.Repositories.Interface;
using Tasks.Domain.Models;
using T = Tasks.Domain.Models.Tasks;
using Task.DAL.EF;
using Tasks.Domain.Models.Tasks;
using Tasks.Domain.Models.ContractorInitiator;

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

    public int AddTask(TasksChangeModel taskContractorInitiator)
    {
        var contractorInitiator = new ContractorInitiator
        {
            ContractorId = taskContractorInitiator.ContractorInitiator.ContractorId,
            InitiatorId = taskContractorInitiator.ContractorInitiator.InitiatorId
        };

        _dbContext.ContractorInitiator.Add(contractorInitiator);
        _dbContext.SaveChanges();

        var task = new T.Task
        {
            Subject = taskContractorInitiator.Task.Subject,
            Description = taskContractorInitiator.Task.Description,
            ExpirationDate = taskContractorInitiator.Task.ExpirationDate.ToUniversalTime(),
            ContractorInitiatorId = contractorInitiator.Id
        };

        _dbContext.Tasks.Add(task);
        _dbContext.SaveChanges();

        return task.Id;
    }

    public T.Task GetTaskById(int id)
    {
        return _dbContext.Tasks.FirstOrDefault(t => t.Id == id);
    }

    public void UpdateTask(TasksChangeModel taskContractorInitiator)
    {
        var contractorInitiator = _dbContext.ContractorInitiator.FirstOrDefault(ci => ci.Id == taskContractorInitiator.Task.ContractorInitiatorId);
        if (contractorInitiator == null)
        {
            return;
        }
        contractorInitiator.ContractorId = taskContractorInitiator.ContractorInitiator.ContractorId;
        contractorInitiator.InitiatorId = taskContractorInitiator.ContractorInitiator.InitiatorId;
        _dbContext.SaveChanges();

        var task = _dbContext.Tasks.FirstOrDefault(t => t.Id == taskContractorInitiator.Task.Id);
        if (task == null)
        {
            return;
        }
        task.Subject = taskContractorInitiator.Task.Subject;
        task.Description = taskContractorInitiator.Task.Description;
        task.ExpirationDate = taskContractorInitiator.Task.ExpirationDate.ToUniversalTime();
        _dbContext.SaveChanges();
    }
}