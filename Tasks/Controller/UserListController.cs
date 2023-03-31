using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Tasks.DAL.Repositories.Interface;
using Tasks.Domain.Models.Users;
using Tasks.Logic;
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

            HttpRequest request = HttpContext.Request;

            var (pageCurrent, pageCount, displayedUsers) = Pagination.GetPagedResult(users, request);

            var model = new UsersModel
            {
                Users = displayedUsers,
                PageCurrent = pageCurrent,
                PageCount = pageCount
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
            user = _userRepository.AddUser(user);
            return RedirectToAction("Change", "UserList", new { id = user.Id });
        }

        [HttpGet]
        public IActionResult Change(int? id)
        {
            var userById = _userRepository.GetUserById(id.Value);
            if (userById == null)
            {
                return NotFound();
            }
            return View(userById);
        }

        [HttpPost]
        public IActionResult Change(User user)
        {
            _userRepository.UpdateUser(user);
            return RedirectToAction("Index", "UserList");
        }
    }
}
