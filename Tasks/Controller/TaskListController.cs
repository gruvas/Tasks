using Microsoft.AspNetCore.Mvc;
using Tasks.DAL.Repositories.Interface;
using Tasks.Domain.Models.ContractorInitiator;
using Tasks.Logic;
using Tasks.Models;
using T = Tasks.Domain.Models.Tasks;

namespace Tasks.Controllers
{
    public class TaskListController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly IContractorInitiatorRepository _contractorInitiatorRepository;

        public TaskListController(ITaskRepository taskRepository, IUserRepository userRepository,
            IContractorInitiatorRepository contractorInitiatorRepository)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
            _contractorInitiatorRepository = contractorInitiatorRepository;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var tasks = _taskRepository.GetAllTasks();
            var contractorInitiator = _contractorInitiatorRepository.GetAll();

            var taskContractorInitiator = Combining.TaskContractorInitiator(tasks, contractorInitiator);

            HttpRequest request = HttpContext.Request;

            var (pageCurrent, pageCount, displayedTasks) = Pagination.GetPagedResult(taskContractorInitiator, request);

            var model = new TasksModel
            {
                TaskContractorInitiator = displayedTasks,
                PageCurrent = pageCurrent,
                PageCount = pageCount
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<int> userIds = _userRepository.GettingIdsUser();

            var model = new T.TasksChangeModel
            {
                UserIds = userIds
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(T.TasksChangeModel taskContractorInitiator)
        {
            List<int> userIds = _userRepository.GettingIdsUser();

            var model = new T.TasksChangeModel
            {
                Task = taskContractorInitiator.Task,
                UserIds = userIds,
                ContractorInitiator = taskContractorInitiator.ContractorInitiator
            };

            int taskId = _taskRepository.AddTask(model);
            return RedirectToAction("Change", "TaskList", new { id = taskId });
        }

        [HttpGet]
        public IActionResult Change(int? id)
        {
            T.Task task = _taskRepository.GetTaskById(id.Value);
            List<int> userIds = _userRepository.GettingIdsUser();
            ContractorInitiator contractorInitiator = _contractorInitiatorRepository.GetById(task.ContractorInitiatorId);

            if (task == null)
            {
                return NotFound();
            }

            var model = new T.TasksChangeModel
            {
                Task = task,
                UserIds = userIds,
                ContractorInitiator = contractorInitiator
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Change(T.TasksChangeModel taskContractorInitiator)
        {
            List<int> userIds = _userRepository.GettingIdsUser();

            var model = new T.TasksChangeModel
            {
                Task = taskContractorInitiator.Task,
                UserIds = userIds,
                ContractorInitiator = taskContractorInitiator.ContractorInitiator
            };

            _taskRepository.UpdateTask(model);

            return RedirectToAction("Index", "TaskList");
        }
    }
}
