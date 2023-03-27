namespace Tasks.Models
{
    public class UsersModel
    {
        public List<User> Users { get; set; }
        public int PageCurrent { get; set; }
        public int PageCount { get; set; }
    }
}
