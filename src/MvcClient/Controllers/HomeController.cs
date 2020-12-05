using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AppCore.Interfaces;
using AppCore.Models;
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
        // private ToDoTask task;
        public IList<User> user_not_joints = new List<User>();
        public TaskViewModel view = new TaskViewModel();
        public HomeController(IUnitOfWork unitOfWork, ILogger<HomeController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var tasks = _unitOfWork.ToDoTasks.GetAll();
            view = new TaskViewModel(tasks);
            return View(view);
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
            view.TaskDetail = task;
            this.user_not_joints = users.Except(joint_users).ToList();
            this.user_not_joints.Remove(task.RegisteredUser);
            view.UserNotJointed = new SelectList(this.user_not_joints, "Id", "Name", this.user_not_joints[0].Id, "RoleName");
            view.Users = new SelectList(users, "Id", "Name", task.RegisteredUserId, "RoleName");

            return View(view);
        }
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public IActionResult UpdateTaskDetail(int id, string val, string action = "")
        {
            var task = _unitOfWork.ToDoTasks.GetBy(id);
            JsonResult rs = new JsonResult(task);
            if (task == null)
            {
                return rs;
            }
            else
            {

                var user = _unitOfWork.Users.GetBy(1);
                switch (action)
                {
                    case "Title":
                        if (val == null)
                        {
                            return rs;
                        }
                        else
                        {
                            task.Title = val;

                            _unitOfWork.ToDoTasks.Update(user, task);


                        }
                        break;
                    case "Describe":
                        if (val == null)
                        {
                            return rs;
                        }
                        else
                        {
                            task.Description = val;
                            _unitOfWork.ToDoTasks.Update(user, task);
                        }
                        break;
                    case "Registered":
                        if (val == null)
                        {
                            return rs;
                        }
                        else
                        {
                            int new_user_id = Int32.Parse(val);
                            task.RegisteredUserId = new_user_id;
                            _unitOfWork.ToDoTasks.Update(user, task);


                        }
                        break;


                    case "Scope":
                        int code = Int32.Parse(val);
                        if (code == 0)
                        {
                            task.Scope = SCOPE.PUBLIC;
                        }
                        else
                        {
                            if (code == 1)
                            {
                                task.Scope = SCOPE.PRIVATE;
                            }

                        }
                        _unitOfWork.ToDoTasks.Update(user, task);
                        break;
                    case "Status":
                        int stt_code = Int32.Parse(val);
                        if (stt_code == 0)
                        {
                            task.Status = STATUS.NEW;
                        }
                        else
                        {
                            if (stt_code == 1)
                            {
                                task.Status = STATUS.ON_PROGRESS;
                            }
                            else
                            {
                                if (stt_code == 2)
                                {
                                    task.Status = STATUS.DONE;
                                }
                            }
                        }
                        _unitOfWork.ToDoTasks.Update(user, task);
                        break;
                    case "Day":
                        if (val == null)
                        {
                            return rs;
                        }
                        else
                        {
                            task.EndDate = DateTime.ParseExact(val, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            _unitOfWork.ToDoTasks.Update(user, task);
                        }
                        break;

                    case "Add Joint User":
                        int joint_user_id = Int32.Parse(val);
                        _unitOfWork.JointUsers.Add(user, new JointUser(joint_user_id, id));
                        task.JointUsers.Add(new JointUser(joint_user_id, id));
                        break;
                    case "Remove Joint User":
                        joint_user_id = Int32.Parse(val);
                        _unitOfWork.JointUsers.Delete(user, new JointUser(joint_user_id, id));
                        task.JointUsers.Remove(new JointUser(joint_user_id, id));
                        break;
                    case "Comment":
                        if (val == null)
                        {
                            return rs;
                        }
                        else
                        {
                            _unitOfWork.Comments.Add(user, new Comment(id, 1, DateTime.Now, val));
                            task.Comments.Add(new Comment(id, 1, DateTime.Now, val));
                        }
                        break;
                    case "File":
                        if (val == null)
                        {
                            return rs;
                        }
                        else
                        {
                            _unitOfWork.AttachedFiles.Add(user, new AttachedFile(id, val));
                            task.AttachedFiles.Add(new AttachedFile(id, val));
                        }
                        break;
                }
                view.TaskDetail = task;
                rs.Value = task;

                return rs;
            }



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