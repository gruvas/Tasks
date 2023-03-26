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

        [HttpGet]
        [HttpPost]
        public IActionResult Change(User user, int? id)
        {
            if (id != null)
            {
                var userById = _userRepository.GetUserById(id.Value);
                if (userById == null)
                {
                    return NotFound();
                }
                return View(userById);
            }
            else 
            {
                user = _userRepository.AddUser(user);
                return RedirectToAction("Change", "UserList", new { id = user.Id });
            }
        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            _userRepository.UpdateUser(user);

            var users = _userRepository.GetAllUsers();

            var model = new UsersModel
            {
                Users = users
            };

            return View(model);
        }
    }
}
