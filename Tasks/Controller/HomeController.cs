using Microsoft.AspNetCore.Mvc;
using Tasks.Models;

namespace Tasks.Controllers
{
    public class HomeController : Controller
    {

        private List<User> users = new List<User>();


        UsersModel userModel = new UsersModel
        {
            Users = new List<User>
            {
                new User { Id = 1, FirstName = "Иван", LastName = "Иванов", Email = "ivan@example.com" },
                new User { Id = 2, FirstName = "Петр", LastName = "Петров", Email = "petr@example.com" },
                new User { Id = 3, FirstName = "Сидор", LastName = "Сидоров", Email = "sidor@example.com" }
            }
        };



        [HttpGet]
        public IActionResult Index()
        {
            var model = userModel;

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            return View(user);
        }


        [HttpPost]
        public IActionResult Index(User user)
        {
            user.Id = userModel.Users.Max(u => u.Id) + 1;
            userModel.Users.Add(user);

            return View(userModel);
        }
    }
}
