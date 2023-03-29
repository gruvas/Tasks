using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Task.DAL.EF;
using Tasks.DAL;
using Tasks.DAL.Repositories.Interface;
using Tasks.Domain.Models.Users;
using System.Linq;
using Microsoft.EntityFrameworkCore;


public class UserRepositoryEF : IUserRepository
{
    private readonly PostgreeContext _context;

    public UserRepositoryEF(PostgreeContext context)
    {
        _context = context;
    }

    public List<User> GetAllUsers()
    {
        return _context.Users.ToList();
    }

    public User GetUserById(int id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id);
    }
    public List<int> GettingIdsTask()
    {
        return _context.Users.Select(t => t.Id).Distinct().ToList();
    }

    public User AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public void UpdateUser(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public void DeleteUser(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}