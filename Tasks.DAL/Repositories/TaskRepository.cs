using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using Tasks.DAL;
using Tasks.DAL.Repositories.Interface;
using Tasks.Models;


public class TaskRepository : ITaskRepository
{
    string connectionString = "";

    public TaskRepository(string connection)
    {
        connectionString = connection;
    }

    public List<Tasks.Models.Task> GetAllTasks()
    {
        using (IDbConnection db = new NpgsqlConnection(connectionString))
        {
            return db.Query<Tasks.Models.Task>("SELECT * FROM main.tasks").ToList();
        }
    }

    public Tasks.Models.Task GetTaskById(int id)
    {
        using (IDbConnection db = new NpgsqlConnection(connectionString))
        {
            string selectQuery = $"SELECT * FROM main.tasks WHERE \"Id\" = {id}";
            return db.QueryFirstOrDefault<Tasks.Models.Task>(selectQuery);
        }
    }

    public Tasks.Models.Task AddTask(Tasks.Models.Task task)
    {
        using (IDbConnection db = new NpgsqlConnection(connectionString))
        {
            string insertQuery = $"INSERT INTO main.tasks(\"Subject\", \"Description\", " +
                $"\"ContractorId\", \"InitiatorId\", \"ExpirationDate\") " +
                $"VALUES('{task.Subject}', '{task.Description}','{task.ContractorId}'," +
                $"'{task.InitiatorId}','{task.ExpirationDate}') " +
                "RETURNING \"Id\", \"Subject\", \"Description\", \"ContractorId\", " +
                "\"InitiatorId\", \"CreatedDate\", \"ExpirationDate\"";
            return db.QueryFirstOrDefault<Tasks.Models.Task>(insertQuery);
        }
    }

    public void UpdateTask(Tasks.Models.Task task)
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