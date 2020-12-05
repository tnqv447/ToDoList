using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AppCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MvcClient.Models;
namespace MvcClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork, ILogger<HomeController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var tasks = _unitOfWork.ToDoTasks.GetAll();
            var TaskView = new TaskViewModel(tasks);
            return View(TaskView);
        }

        public IActionResult Create()
        {
            return View();
        }

        [Route("Home/TaskDetail/{taskId:int}")]
        public IActionResult TaskDetail(int taskId)
        {
            var task = _unitOfWork.ToDoTasks.GetBy(taskId);
            var users = _unitOfWork.Users.GetAll();
            var joint_users = task.JointUsers.Select(item => item.User).ToList();
            var view = new TaskViewModel();
            view.TaskDetail = task;
            var user_not_joints = users.Except(joint_users).ToList();
            view.UserNotJointed = new SelectList(user_not_joints, "Id", "Name", user_not_joints.FirstOrDefault(), "Role");
            return View(view);
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Privacy()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}