using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using Tasks.DAL.Repositories.Interface;
using Task = Tasks.Domain.Models.Tasks.Task;

public class TaskRepository : ITaskRepository
{
    string connectionString = "";

    public TaskRepository(string connection)
    {
        connectionString = connection;
    }

    public Task AddTask(Task task)
    {
        using (IDbConnection db = new NpgsqlConnection(connectionString))
        {
            string insertQuery = $"INSERT INTO main.tasks(\"Subject\", \"Description\", " +
                $"\"ContractorId\", \"InitiatorId\", \"ExpirationDate\") " +
                $"VALUES('{task.Subject}', '{task.Description}','{task.ContractorId}'," +
                $"'{task.InitiatorId}','{task.ExpirationDate}') " +
                "RETURNING \"Id\", \"Subject\", \"Description\", \"ContractorId\", " +
                "\"InitiatorId\", \"CreatedDate\", \"ExpirationDate\"";
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

    public List<int> GettingIdsTask()
    {
        using (IDbConnection db = new NpgsqlConnection(connectionString))
        {
            return db.Query<int>("SELECT \"Id\" FROM main.tasks").ToList();
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


    public void UpdateTask(Task task)
    {
        using (IDbConnection db = new NpgsqlConnection(connectionString))
        {
            string updateQuery = $"UPDATE main.tasks SET \"Subject\" =" +
                $" '{task.Subject}', \"Description\" = '{task.Description}', " +
                $"\"ContractorId\" = '{task.ContractorId}', \"InitiatorId\" = " +
                $"'{task.InitiatorId}', \"CreatedDate\" = '{task.CreatedDate}', " +
                $"\"ExpirationDate\" = '{task.ExpirationDate}' WHERE \"Id\" = {task.Id}";
            db.Execute(updateQuery);
        }
    }

    public void DeleteTask(int id)
    {
        using (IDbConnection db = new NpgsqlConnection(connectionString))
        {
            string deleteQuery = $"DELETE FROM main.tasks WHERE \"Id\" = {id}";
            db.Execute(deleteQuery);
        }
    }
}