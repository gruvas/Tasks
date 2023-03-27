using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Tasks.DAL.Repositories.Interface;
using Tasks.Logic;
using Tasks.Models;
using Task = Tasks.Models.Task;

namespace Tasks.Controllers
{
    public class TaskListController : Controller
    {
        private readonly ITaskRepository _taskRepository;

        public TaskListController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
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
            return View();
        }

        [HttpPost]
        public IActionResult Create(Task task)
        {
            return View(task);
        }

        [HttpGet]
        [HttpPost]
        public IActionResult Change(Task task, int? id)
        {
            if (id != null)
            {
                var taskById = _taskRepository.GetTaskById(id.Value);
                if (taskById == null)
                {
                    return NotFound();
                }
                return View(taskById);
            }
            else 
            {
                task = _taskRepository.AddTask(task);
                return RedirectToAction("Change", "TaskList", new { id = task.Id });
            }
        }

        [HttpPost]
        public IActionResult Index(Task task)
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
