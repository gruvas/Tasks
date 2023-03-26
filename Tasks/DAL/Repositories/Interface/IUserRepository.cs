using Tasks.Models;

namespace Tasks.DAL.Repositories.Interface
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUserById(int id);
        User AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}
