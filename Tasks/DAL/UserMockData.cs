using Tasks.Models;

namespace Tasks.DAL
{
    public class UserMockData
    {
        private List<User> _users;
        public UserMockData()
        {
            _users = new List<User>();

            for (int i = 1; i < 10; i++)
            {
                _users.Add(new User()
                {
                    Id = i,
                    LastName = $"LastName {i}",
                    FirstName = $"FirstName {i}",
                    Email = $"Email {i}"
                });
            }
        }

        public List<User> Users => _users;
    }
}
