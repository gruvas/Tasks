using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using Tasks.DAL.Repositories.Interface;
using Tasks.Logic;
using Tasks.Models;
using Tasks.Domain.Models.Tasks;
using T = Tasks.Domain.Models.Tasks;
using Tasks.Domain.Models.ContractorInitiator;
using static Azure.Core.HttpHeader;

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

            var taskContractorInitiator = new List<TaskContractorInitiator>();


            for (int i = 0; i < tasks.Count; i++)
            {
                for (int ii = 0; ii < contractorInitiator.Count; ii++)
                {
                    if (tasks[i].ContractorInitiatorId == contractorInitiator[ii].Id)
                    {
                        taskContractorInitiator.Add(new TaskContractorInitiator
                        {
                            Task = tasks[i],
                            СontractorInitiator = contractorInitiator[ii]
                        });
                    }
                }  
            }

            HttpRequest request = HttpContext.Request;

            var (pageCurrent, pageCount, displayedTasks) = Pagination.GetPagedResult(tasks, request);

            var model = new TasksModel
            {
                TaskContractorInitiator = taskContractorInitiator,
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
            T.Task taskById = _taskRepository.GetTaskById(id.Value);
            List<int> userIds = _userRepository.GettingIdsUser();

            if (taskById == null)
            {
                return NotFound();
            }

            var model = new T.TasksChangeModel
            {
                Task = taskById,
                UserIds = userIds
            };

            return View(model);
        }

        //[HttpPost]
        //public IActionResult Index(T.Task task)
        //{
        //    //_taskRepository.UpdateTask(task);

        //    var tasks = _taskRepository.GetAllTasks();

        //    HttpRequest request = HttpContext.Request;

        //    var (pageCurrent, pageCount, displayedTasks) = Pagination.GetPagedResult(tasks, request);

        //    var model = new TasksModel
        //    {
        //        Tasks = displayedTasks,
        //        PageCurrent = pageCurrent,
        //        PageCount = pageCount
        //    };

        //    return View(model);
        //}
    }
}
