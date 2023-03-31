using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using Tasks.DAL.Repositories.Interface;
using Tasks.Domain.Models.ContractorInitiator;
using Tasks.Domain.Models.Tasks;
using Task = Tasks.Domain.Models.Tasks.Task;

public class TaskRepository : ITaskRepository
{
    string connectionString = "";

    public TaskRepository(string connection)
    {
        connectionString = connection;
    }

    public int AddTask(TasksChangeModel taskContractorInitiator)
    {
        using (IDbConnection db = new NpgsqlConnection(connectionString))
        {
            string insertContractorInitiatorQuery = $"INSERT INTO main.contractor_initiator(\"ContractorId\", \"InitiatorId\") " +
                $"VALUES('{taskContractorInitiator.ContractorInitiator.ContractorId}', '{taskContractorInitiator.ContractorInitiator.InitiatorId}')" +
                $" RETURNING \"Id\"";

            int contractorInitiator = db.ExecuteScalar<int>(insertContractorInitiatorQuery);

            string insertTaskQuery = $"INSERT INTO main.tasks(\"Subject\", \"Description\", \"ExpirationDate\", \"ContractorInitiatorId\") " +
                $"VALUES('{taskContractorInitiator.Task.Subject}', '{taskContractorInitiator.Task.Description}', " +
                $"'{taskContractorInitiator.Task.ExpirationDate}', '{contractorInitiator}') RETURNING \"Id\"";

            int taskId = db.QueryFirstOrDefault<int>(insertTaskQuery);

            return taskId;
        }
    }

    public List<Task> GetAllTasks()
    {
        using (IDbConnection db = new NpgsqlConnection(connectionString))
        {
            return db.Query<Task>("SELECT * FROM main.tasks").ToList();
        }
    }

    public Task GetTaskById(int id)
    {
        using (IDbConnection db = new NpgsqlConnection(connectionString))
        {
            string selectQuery = $"SELECT * FROM main.tasks WHERE \"Id\" = {id}";
            return db.QueryFirstOrDefault<Task>(selectQuery);
        }
    }

    public void UpdateTask(TasksChangeModel taskContractorInitiator)
    {
        using (IDbConnection db = new NpgsqlConnection(connectionString))
        {
            string updateContractorInitiatorQuery = $"UPDATE main.contractor_initiator " +
            $"SET \"ContractorId\" = '{taskContractorInitiator.ContractorInitiator.ContractorId}', " +
            $"\"InitiatorId\" = '{taskContractorInitiator.ContractorInitiator.InitiatorId}' " +
            $"WHERE \"Id\" = '{taskContractorInitiator.Task.ContractorInitiatorId}'";

            db.Execute(updateContractorInitiatorQuery);

            string updateTaskQuery = $"UPDATE main.tasks " +
                $"SET \"Subject\" = '{taskContractorInitiator.Task.Subject}', " +
                $"\"Description\" = '{taskContractorInitiator.Task.Description}', " +
                $"\"ExpirationDate\" = '{taskContractorInitiator.Task.ExpirationDate}' " +
                $"WHERE \"Id\" = '{taskContractorInitiator.Task.Id}'";

            int updatedRows = db.Execute(updateTaskQuery);
        }
    }
}