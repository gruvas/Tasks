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
    public UserMockData _userModel;

    //string connectionString = null;

    //public UserRepository(string conn)
    //{
    //    connectionString = conn;
    //}

    string connectionString = "Server=localhost;Port=5432;Database=c#;User Id=postgres;Password=root;";


    public UserRepository(UserMockData userModel)
    {
        _userModel = userModel;
    }

    public List<User> GetAllUsers()
    {
        //return _userModel.Users;

        using (IDbConnection db = new NpgsqlConnection(connectionString))
        {
            return db.Query<User>("SELECT * FROM main.users").ToList();
        }
    }

    public User GetUserById(int id)
    {
        return _userModel.Users.FirstOrDefault(u => u.Id == id);
    }

    public void AddUser(User user)
    {
        //user.Id = _userModel.Users.Max(u => u.Id) + 1;
        //_userModel.Users.Add(user);

        using (IDbConnection db = new NpgsqlConnection(connectionString))
        {
            string insertQuery = $"INSERT INTO main.users(\"lastname\", \"firstname\", \"email\") " +
                                 $"VALUES('{user.LastName}', '{user.FirstName}','{user.Email}')";
            db.Execute(insertQuery);
        }


    }

    public void UpdateUser(User user)
    {
        var existingUser = _userModel.Users.FirstOrDefault(u => u.Id == user.Id);
        if (existingUser != null)
        {
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
        }
    }

    public void DeleteUser(int id)
    {
        var userToDelete = _userModel.Users.FirstOrDefault(u => u.Id == id);
        if (userToDelete != null)
        {
            _userModel.Users.Remove(userToDelete);
        }
    }
}