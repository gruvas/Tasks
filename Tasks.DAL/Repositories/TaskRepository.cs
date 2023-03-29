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
            // Insert task and get its id
            string insertTaskQuery = @"INSERT INTO main.tasks(""Subject"", ""Description"", ""ContractorId"", ""InitiatorId"", ""ExpirationDate"") 
                                   VALUES(@Subject, @Description, @ContractorId, @InitiatorId, @ExpirationDate) 
                                   RETURNING ""Id"", ""Subject"", ""Description"", ""ContractorId"", ""InitiatorId"", ""CreatedDate"", ""ExpirationDate""";
            Task insertedTask = db.QueryFirstOrDefault<Task>(insertTaskQuery, new
            {
                Subject = task.Subject,
                Description = task.Description,
                ContractorId = task.ContractorId,
                InitiatorId = task.InitiatorId,
                ExpirationDate = task.ExpirationDate
            });

            // Insert user-task mapping
            string insertUserTaskQuery = @"INSERT INTO main.users_tasks(""UserId"", ""TaskId"") 
                                       VALUES(@UserId, @TaskId) RETURNING ""Id""";
            int userTaskId = db.ExecuteScalar<int>(insertUserTaskQuery, new
            {
                UserId = task.InitiatorId,
                TaskId = insertedTask.Id
            });

            // Update task with user-task mapping id
            string updateQuery = $"UPDATE main.tasks SET \"UserTaskId\" = '{userTaskId}' WHERE \"Id\" = {insertedTask.Id}";
            db.Execute(updateQuery);

            return insertedTask;
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
            string updateQuery = @"UPDATE main.tasks SET 
                                ""Subject"" = @Subject,
                                ""Description"" = @Description, 
                                ""ContractorId"" = @ContractorId, 
                                ""InitiatorId"" = @InitiatorId, 
                                ""CreatedDate"" = @CreatedDate,
                                ""ExpirationDate"" = @ExpirationDate,
                                ""UserTaskId"" = @UserTaskId 
                              WHERE ""Id"" = @Id";

            db.Execute(updateQuery, new
            {
                Subject = task.Subject,
                Description = task.Description,
                ContractorId = task.ContractorId,
                InitiatorId = task.InitiatorId,
                CreatedDate = task.CreatedDate,
                ExpirationDate = task.ExpirationDate,
                UserTaskId = task.UserTaskId,
                Id = task.Id
            });

            // Update user_task mapping
            string updateUserTaskQuery = @"UPDATE main.users_tasks SET 
                                        ""UserId"" = @UserId
                                      WHERE ""TaskId"" = @TaskId";
            db.Execute(updateUserTaskQuery, new
            {
                UserId = task.InitiatorId,
                TaskId = task.Id
            });
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