using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Tasks.DAL;
using Tasks.DAL.Repositories.Interface;
using Tasks.Models;


public class UserRepository : IUserRepository
{
    string connectionString = "";

    public UserRepository(string connection)
    {
        connectionString = connection;
    }

    public List<User> GetAllUsers()
    {
        using (IDbConnection db = new NpgsqlConnection(connectionString))
        {
            return db.Query<User>("SELECT * FROM main.users").ToList();
        }
    }

    public User GetUserById(int id)
    {
        using (IDbConnection db = new NpgsqlConnection(connectionString))
        {
            string selectQuery = $"SELECT * FROM main.users WHERE \"Id\" = {id}";
            return db.QueryFirstOrDefault<User>(selectQuery);
        }
    }

    public User AddUser(User user)
    {
        using (IDbConnection db = new NpgsqlConnection(connectionString))
        {
            string insertQuery = $"INSERT INTO main.users(\"LastName\", \"FirstName\", \"Email\") " +
                                 $"VALUES('{user.LastName}', '{user.FirstName}','{user.Email}')" +
                             "RETURNING \"Id\", \"LastName\", \"FirstName\", \"Email\"";
            return db.QueryFirstOrDefault<User>(insertQuery);
        }
    }

    public void UpdateUser(User user)
    {
        using (IDbConnection db = new NpgsqlConnection(connectionString))
        {
            string updateQuery = $"UPDATE main.users SET \"LastName\" = '{user.LastName}', \"FirstName\" = '{user.FirstName}', \"Email\" = '{user.Email}' WHERE \"Id\" = {user.Id}";
            db.Execute(updateQuery);
        }
    }

    public void DeleteUser(int id)
    {
        using (IDbConnection db = new NpgsqlConnection(connectionString))
        {
            string deleteQuery = $"DELETE FROM main.users WHERE \"Id\" = {id}";
            db.Execute(deleteQuery);
        }
    }
}