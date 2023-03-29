using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Tasks.DAL.Repositories.Interface;
using Tasks.Logic;
using Tasks.Models;
using T = Tasks.Domain.Models.Tasks;

namespace Tasks.Controllers
{
    public class TaskListController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;

        public TaskListController(ITaskRepository taskRepository, IUserRepository userRepository)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var tasks = _taskRepository.GetAllTasks();

            HttpRequest request = HttpContext.Request;

            var (pageCurrent, pageCount, displayedTasks) = Pagination.GetPagedResult(tasks, request);

            var model = new TasksModel
            {
                Tasks = displayedTasks,
                PageCurrent = pageCurrent,
                PageCount = pageCount
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<int> userIds = _userRepository.GettingIdsTask();

            var model = new TasksChangeModel
            {
                Task = null,
                UserIds = userIds
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(T.Task task)
        {
            return View(task);
        }

        [HttpGet]
        [HttpPost]
        public IActionResult Change(T.Task task, int? id)
        {
            if (id != null)
            {
                T.Task taskById = _taskRepository.GetTaskById(id.Value);
                List<int> userIds = _userRepository.GettingIdsTask();

                if (taskById == null)
                {
                    return NotFound();
                }

                var model = new TasksChangeModel
                {
                    Task = taskById,
                    UserIds = userIds
                };

                return View(model);
            }
            else 
            {
                task = _taskRepository.AddTask(task);
                return RedirectToAction("Change", "TaskList", new { id = task.Id });
            }
        }

        [HttpPost]
        public IActionResult Index(T.Task task)
        {
            _taskRepository.UpdateTask(task);

            var tasks = _taskRepository.GetAllTasks();

            HttpRequest request = HttpContext.Request;

            var (pageCurrent, pageCount, displayedTasks) = Pagination.GetPagedResult(tasks, request);

            var model = new TasksModel
            {
                Tasks = displayedTasks,
                PageCurrent = pageCurrent,
                PageCount = pageCount
            };

            return View(model);
        }
    }
}
