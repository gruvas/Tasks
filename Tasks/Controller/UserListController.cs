using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tasks.DAL.Repositories.Interface;
using Tasks.Models;

namespace Tasks.Controllers
{
    public class UserListController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserListController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = _userRepository.GetAllUsers();

            var model = new UsersModel
            {
                Users = users
            };

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
            _userRepository.AddUser(user);

            var users = _userRepository.GetAllUsers();

            var model = new UsersModel
            {
                Users = users
            };

            return View(model);
        }
    }
}
