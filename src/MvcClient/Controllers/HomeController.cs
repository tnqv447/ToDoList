using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AppCore.Interfaces;
using AppCore.Models;
using AppCore.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly ISearchSortService _service;
        // private ToDoTask task;
        private IList<User> user_not_joints;
        private PaginatedList<ToDoTask> tasks;
        private TaskViewModel view;
        private int userId;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(IUnitOfWork unitOfWork, ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment,
        ISearchSortService service)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _service = service;
        }
        public IActionResult Index(int pageNumber = 1, string searchString = null)
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                return Redirect("Login");
            }
            else
            {

                view = GetTaskViewModel(searchString, pageNumber);
                return View(view);
            }
        }
        public TaskViewModel GetTaskViewModel(string searchString = null, int pageNumber = 1)
        {
            var pageSize = 6;
            var role = HttpContext.Session.GetString("role");
            IList<ToDoTask> temp = new List<ToDoTask>();
            if (role.Equals("manager"))
            {
                temp = _unitOfWork.ToDoTasks.GetAll();
            }
            else
            {
                temp = _unitOfWork.ToDoTasks.GetTasksForUser(HttpContext.Session.GetInt32("id").GetValueOrDefault());
            }
            if (searchString == null || searchString.Equals(""))
            {
                tasks = PaginatedList<ToDoTask>.Create(temp, pageNumber, pageSize);
            }
            else
            {
                var searchResult = _service.Search(temp, searchString, null);
                tasks = PaginatedList<ToDoTask>.Create(searchResult, pageNumber, pageSize);
            }
            TaskViewModel taskView = new TaskViewModel(tasks);
            return taskView;
        }
        public IActionResult Paging(string searchString, int pageNumber)
        {
            view = GetTaskViewModel(searchString, pageNumber);
            return PartialView("_TaskList", view);
        }
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("id").GetValueOrDefault();
            var user = _unitOfWork.Users.GetBy(userId);
            view = new TaskViewModel();

            if (HttpContext.Session.GetString("role").Equals("manager"))
            {
                var users = _unitOfWork.Users.GetAll();
                view.Users = new SelectList(users, "Id", "Name", users.FirstOrDefault(), "RoleName");
            }
            view.Name = user.Name;
            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewTask(ToDoTask task, string role)
        {
            var userId = HttpContext.Session.GetInt32("id").GetValueOrDefault();
            var user = _unitOfWork.Users.GetBy(userId);
            IList<ToDoTask> temp = new List<ToDoTask>();
            if (ModelState.IsValid)
            {
                _unitOfWork.ToDoTasks.Add(user, new ToDoTask(task));
                if (role.Equals("worker"))
                {
                    temp = _unitOfWork.ToDoTasks.GetTasksForUser(userId);
                }
                else
                {
                    if (role.Equals("manager"))
                    {
                        temp = _unitOfWork.ToDoTasks.GetAll();
                    }
                }
                var pageNumber = (int)Math.Ceiling(temp.Count() / (double)6);
                tasks = PaginatedList<ToDoTask>.Create(temp, pageNumber, 6);
                view = new TaskViewModel(tasks);

            }
            return PartialView("_TaskList", view);

        }
        [HttpPost]
        public IActionResult DeleteTask(int taskId, int pageNumber)
        {
            var delTask = _unitOfWork.ToDoTasks.GetBy(taskId);
            var user = _unitOfWork.Users.GetBy(HttpContext.Session.GetInt32("id").GetValueOrDefault());
            _unitOfWork.ToDoTasks.Delete(user, delTask);
            view = GetTaskViewModel(null, pageNumber);
            return PartialView("_TaskList", view);
        }
        [Route("Home/TaskDetail/{taskId:int}")]

        public IActionResult TaskDetail(int taskId)
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                userId = HttpContext.Session.GetInt32("id").GetValueOrDefault();
                view = new TaskViewModel();
                var task = _unitOfWork.ToDoTasks.GetBy(taskId);
                this.user_not_joints = _unitOfWork.Users.GetUserNotJointForTask(task.Id);
                var users = _unitOfWork.Users.GetAll();

                view.isInTask = _unitOfWork.ToDoTasks.CheckUserInTask(userId, task);
                view.TaskDetail = task;
                this.user_not_joints.Remove(task.RegisteredUser);
                view.UserNotJointed = new SelectList(this.user_not_joints, "Id", "Name", this.user_not_joints[0].Id, "RoleName");
                view.Users = new SelectList(users, "Id", "Name", task.RegisteredUserId, "RoleName");

                return View(view);
            }

        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public IActionResult UpdateTaskDetail(int id, string val, string action = "")
        {
            var task = _unitOfWork.ToDoTasks.GetBy(id);
            var response = "success";
            JsonResult rs = new JsonResult(task);
            view = new TaskViewModel();
            Dictionary<string, object> temp = new Dictionary<string, object>();
            if (task == null)
            {
                temp.Clear();
                response = "failed";
                temp.Add("check", response);
                rs.Value = temp;
                return rs;
            }
            else
            {
                userId = HttpContext.Session.GetInt32("id").GetValueOrDefault();
                var user = _unitOfWork.Users.GetBy(userId);
                var check = _unitOfWork.ToDoTasks.CheckUserInTask(userId, task);
                if (check)
                {
                    switch (action)
                    {
                        case "Title":
                            if (val == null)
                            {
                                temp.Clear();
                                response = "failed";
                                temp.Add("check", response);
                                rs.Value = temp;
                                return rs;
                            }
                            else
                            {
                                temp.Clear();

                                task.Title = val;
                                _unitOfWork.ToDoTasks.Update(user, task, CHANGE_FIELD.TITLE);
                                temp.Add("check", response);
                                temp.Add("title", task.Title);
                                rs.Value = temp;

                            }
                            break;
                        case "Describe":
                            if (val == null)
                            {
                                temp.Clear();
                                response = "failed";
                                temp.Add("check", response);
                                rs.Value = temp;
                                return rs;
                            }
                            else
                            {
                                temp.Clear();
                                task.Description = val;
                                _unitOfWork.ToDoTasks.Update(user, task, CHANGE_FIELD.DESCRIPTION);
                                temp.Add("check", response);
                                temp.Add("describe", task.Description);
                                rs.Value = temp;
                            }
                            break;
                        case "Registered":
                            if (val == null)
                            {
                                temp.Clear();
                                response = "failed";
                                temp.Add("check", response);
                                rs.Value = temp;
                                return rs;
                            }
                            else
                            {
                                var msg = "no";
                                int new_user_id = Int32.Parse(val);
                                temp.Clear();

                                temp.Add("OldRole", task.RegisteredUser.RoleName);
                                temp.Add("OldUserId", task.RegisteredUserId);
                                temp.Add("OldName", task.RegisteredUserName);
                                task.RegisteredUserId = new_user_id;
                                _unitOfWork.ToDoTasks.Update(user, task, CHANGE_FIELD.REGISTERED_USER);

                                if (task.JointUsers.Any(m => m.UserId == new_user_id && m.ToDoTaskId == id))
                                {
                                    var user_del = task.JointUsers.Where(m => m.UserId == new_user_id && m.ToDoTaskId == id).FirstOrDefault();
                                    msg = "yes";
                                    _unitOfWork.JointUsers.Delete(user, user_del);
                                    // task.JointUsers.Remove(temp_joint_user);

                                }
                                temp.Add("check", response);
                                temp.Add("message", msg);
                                temp.Add("NewUserId", task.RegisteredUserId);

                                rs.Value = temp;
                            }
                            break;
                        case "Scope":
                            int code = Int32.Parse(val);
                            if (code.Equals(0))
                            {
                                task.Scope = SCOPE.PUBLIC;
                                _unitOfWork.ToDoTasks.SetPublic(user, task);
                            }
                            else
                            {
                                if (code.Equals(1))
                                {
                                    task.Scope = SCOPE.PRIVATE;
                                    _unitOfWork.ToDoTasks.SetPrivate(user, task);
                                }
                            }
                            temp.Clear();
                            temp.Add("check", response);
                            temp.Add("scope", task.ScopeName);
                            rs.Value = temp;
                            break;
                        case "Status":
                            int stt_code = Int32.Parse(val);
                            if (stt_code.Equals(0))
                            {
                                task.Status = STATUS.NEW;
                                _unitOfWork.ToDoTasks.SetNew(user, task);
                            }
                            else
                            {
                                if (stt_code.Equals(1))
                                {
                                    task.Status = STATUS.ON_PROGRESS;
                                    _unitOfWork.ToDoTasks.SetOnProgress(user, task);
                                }
                                else
                                {
                                    if (stt_code.Equals(2))
                                    {
                                        task.Status = STATUS.DONE;
                                        _unitOfWork.ToDoTasks.SetDone(user, task);
                                    }
                                }
                            }
                            temp.Clear();
                            temp.Add("check", response);
                            temp.Add("status", task.StatusName);
                            rs.Value = temp;
                            break;
                        case "Day":
                            if (val == null)
                            {

                                temp.Clear();
                                response = "failed";
                                temp.Add("check", response);
                                rs.Value = temp;
                                return rs;
                            }
                            else
                            {
                                task.EndDate = DateTime.ParseExact(val, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                _unitOfWork.ToDoTasks.Update(user, task, CHANGE_FIELD.TIME);
                            }
                            temp.Clear();
                            temp.Add("check", response);
                            temp.Add("date", task.EndDate);
                            rs.Value = temp;

                            break;
                        case "Add Joint User":
                            int joint_user_id = Int32.Parse(val);
                            _unitOfWork.JointUsers.Add(user, new JointUser(joint_user_id, id));
                            task.JointUsers.Add(new JointUser(joint_user_id, id));
                            temp.Clear();
                            temp.Add("id", joint_user_id);
                            temp.Add("check", response);
                            rs.Value = temp;
                            break;
                        case "Remove Joint User":
                            joint_user_id = Int32.Parse(val);
                            var temp_user = task.JointUsers.Where(m => m.ToDoTaskId == id && m.UserId == joint_user_id).FirstOrDefault();
                            _unitOfWork.JointUsers.Delete(user, temp_user);
                            task.JointUsers.Remove(temp_user);
                            temp.Clear();
                            temp.Add("check", response);
                            temp.Add("id", joint_user_id);
                            rs.Value = temp;
                            break;
                        case "Comment":
                            if (val == null)
                            {
                                return rs;
                            }
                            else
                            {
                                _unitOfWork.Comments.Add(user, new Comment(id, userId, DateTime.Now, val));
                                task.Comments.Add(new Comment(id, userId, DateTime.Now, val));
                            }
                            temp.Clear();
                            temp.Add("check", response);
                            temp.Add("comment", task.Comments);
                            rs.Value = temp;


                            break;

                    }
                }
                else
                {
                    temp.Clear();
                    response = "failed";
                    temp.Add("check", response);
                    rs.Value = temp;
                    return rs;
                }
                view.TaskDetail = task;
                // rs.Value = task;

                return rs;
            }
        }
        [HttpPost]
        public IActionResult UploadFile(IFormFile file, int id)
        {
            JsonResult rs = new JsonResult("");
            Dictionary<string, object> temp = new Dictionary<string, object>();
            var response = "success";
            if (file == null)
            {
                response = "failed";
                temp.Add("check", response);
            }
            else
            {
                var userId = HttpContext.Session.GetInt32("id").GetValueOrDefault();
                var user = _unitOfWork.Users.GetBy(userId);


                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "files/");
                string filePath = Path.Combine(uploadsFolder, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                };
                _unitOfWork.AttachedFiles.Add(user, new AttachedFile(id, file.FileName));
                temp.Add("check", response);
                temp.Add("file", file.FileName);
                rs.Value = temp;
            }
            return rs;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchTask(string searchString, int pageNumber = 1)
        {
            var role = HttpContext.Session.GetString("role");
            var userId = HttpContext.Session.GetInt32("id").GetValueOrDefault();
            view = GetTaskViewModel(searchString, pageNumber);
            return PartialView("_TaskList", view);
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