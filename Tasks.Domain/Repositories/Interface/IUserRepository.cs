using Tasks.Domain.Models.Users;

namespace Tasks.DAL.Repositories.Interface
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUserById(int id);
        List<int> GettingIdsTask();
        User AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}
