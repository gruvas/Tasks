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

    public Task AddTask(TasksChangeModel tasksChangeModel)
    {
        using (IDbConnection db = new NpgsqlConnection(connectionString))
        {
            //string insertQuery = $"INSERT INTO main.tasks(\"Subject\", \"Description\", " +
            //    $"\"ExpirationDate\") " +
            //    $"VALUES('{task.Subject}', '{task.Description}'," +
            //    $"'{task.ExpirationDate}') " +
            //    "RETURNING \"Id\", \"Subject\", \"Description\", " +
            //    "\"CreatedDate\", \"ExpirationDate\"";

            string insertQuery = $"WITH inserted AS (INSERT INTO main.tasks(\"Subject\", \"Description\", \"ExpirationDate\") " +
                     $"VALUES('{tasksChangeModel.Task.Subject}', '{tasksChangeModel.Task.Description}', '{tasksChangeModel.Task.ExpirationDate}') " +
                     "RETURNING \"Id\") " +
                     $"INSERT INTO main.contractor_initiator(\"TaskId\", \"ContractorId\", \"InitiatorId\") " +
                     $"SELECT inserted.\"Id\", {tasksChangeModel.ContractorInitiator.ContractorId}, {tasksChangeModel.ContractorInitiator.InitiatorId} " +
                     "FROM inserted " +
                     "RETURNING \"TaskId\", \"ContractorId\", \"InitiatorId\"";


            return db.QueryFirstOrDefault<Task>(insertQuery);
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


    public void UpdateTask(TasksChangeModel tasksChangeModel)
    {
        using (IDbConnection db = new NpgsqlConnection(connectionString))
        {
            //string updateQuery = $"UPDATE main.tasks SET \"Subject\" =" +
            //    $" '{task.Subject}', \"Description\" = '{task.Description}', " +
            //    $"\"ContractorId\" = '{task.ContractorId}', \"InitiatorId\" = " +
            //    $"'{task.InitiatorId}', \"CreatedDate\" = '{task.CreatedDate}', " +
            //    $"\"ExpirationDate\" = '{task.ExpirationDate}' WHERE \"Id\" = {task.Id}";

            string updateQuery = $"UPDATE main.tasks SET " +
                $"\"Subject\" = '{tasksChangeModel.Task.Subject}', " +
                $"\"Description\" = '{tasksChangeModel.Task.Description}', " +
                $"\"ExpirationDate\" = '{tasksChangeModel.Task.ExpirationDate}' " +
                $"WHERE \"Id\" = {tasksChangeModel.Task.Id} " +
                "RETURNING \"Id\", \"Subject\", \"Description\", \"CreatedDate\", \"ExpirationDate\"";

            string contractorInitiatorQuery = $"UPDATE main.contractor_initiator SET " +
                $"\"ContractorId\" = {tasksChangeModel.ContractorInitiator.ContractorId}, " +
                $"\"InitiatorId\" = {tasksChangeModel.ContractorInitiator.InitiatorId} " +
                $"WHERE \"Id\" = {tasksChangeModel.Task.ContractorInitiatorId} " +
                "RETURNING \"TaskId\", \"ContractorId\", \"InitiatorId\"";

            db.QueryFirstOrDefault<ContractorInitiator>(contractorInitiatorQuery);


            db.Execute(updateQuery);
        }
    }

    public void DeleteTask(int id)
    {
        using (IDbConnection db = new NpgsqlConnection(connectionString))
        {
            // Get the UserTaskId for this task
            string getUserTaskIdQuery = $"SELECT \"UserTaskId\" FROM main.tasks WHERE \"Id\" = {id}";
            int userTaskId = db.QuerySingle<int>(getUserTaskIdQuery);

            string deleteQuery = $"DELETE FROM main.tasks WHERE \"Id\" = {id}";
            db.Execute(deleteQuery);

            // Delete user_task mapping
            string deleteUserTaskQuery = $"DELETE FROM main.users_tasks WHERE \"UserTaskId\" = {userTaskId}";
            db.Execute(deleteUserTaskQuery);
        }
    }
}