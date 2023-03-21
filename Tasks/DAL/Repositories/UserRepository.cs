using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;
using System.Data;
using System.Diagnostics;
using Tasks.DAL;
using Tasks.DAL.Repositories.Interface;
using Tasks.Models;

//public interface IUserRepository
//{
//    List<User> GetAllUsers();
//    User GetUserById(int id);
//    void AddUser(User user);
//    void UpdateUser(User user);
//    void DeleteUser(int id);
//}

public class UserRepository : IUserRepository
{
    public UserMockData _userModel;

    //string connectionString = null;
    //string connectionString = "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=root;";
    //string connectionString = "Server = localhost; Port = 5432; Database = c#; Username = postgres; Password = root";
    //string connectionString = "jdbc:postgresql://localhost:5432/postgres";

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
        user.Id = _userModel.Users.Max(u => u.Id) + 1;
        _userModel.Users.Add(user);
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