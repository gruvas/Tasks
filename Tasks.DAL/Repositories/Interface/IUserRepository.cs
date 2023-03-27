using Tasks.Models;

namespace Tasks.DAL.Repositories.Interface
{
    public interface IUserRepository
    {
        List<Models.User> GetAllUsers();
        Models.User GetUserById(int id);
        Models.User AddUser(Models.User user);
        void UpdateUser(Models.User user);
        void DeleteUser(int id);
    }
}
