using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AppCore.Interfaces;
using AppCore.Models;
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
        // private ToDoTask task;
        public IList<User> user_not_joints = new List<User>();
        public TaskViewModel view = new TaskViewModel();
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(IUnitOfWork unitOfWork, ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
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
            Dictionary<string, object> temp = new Dictionary<string, object>();
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

                            _unitOfWork.ToDoTasks.Update(user, task, CHANGE_FIELD.TITLE);
                            rs.Value = task.Title;

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
                            _unitOfWork.ToDoTasks.Update(user, task, CHANGE_FIELD.DESCRIPTION);
                            rs.Value = task.Description;
                        }
                        break;
                    case "Registered":
                        if (val == null)
                        {
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

                            temp.Add("message", msg);
                            temp.Add("NewUserId", task.RegisteredUserId);

                            rs.Value = temp;
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
                        rs.Value = task.ScopeName;
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
                        rs.Value = task.StatusName;
                        break;

                    case "Day":
                        if (val == null)
                        {
                            return rs;
                        }
                        else
                        {
                            task.EndDate = DateTime.ParseExact(val, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            _unitOfWork.ToDoTasks.Update(user, task, CHANGE_FIELD.TIME);
                        }
                        rs.Value = task.EndDate;
                        break;

                    case "Add Joint User":
                        int joint_user_id = Int32.Parse(val);
                        _unitOfWork.JointUsers.Add(user, new JointUser(joint_user_id, id));
                        task.JointUsers.Add(new JointUser(joint_user_id, id));
                        temp.Clear();
                        temp.Add("id", joint_user_id);

                        rs.Value = temp;
                        break;

                    case "Remove Joint User":
                        joint_user_id = Int32.Parse(val);
                        var temp_user = task.JointUsers.Where(m => m.ToDoTaskId == id && m.UserId == joint_user_id).FirstOrDefault();
                        _unitOfWork.JointUsers.Delete(user, temp_user);
                        task.JointUsers.Remove(temp_user);
                        temp.Clear();
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
                            _unitOfWork.Comments.Add(user, new Comment(id, 1, DateTime.Now, val));
                            task.Comments.Add(new Comment(id, 1, DateTime.Now, val));
                        }
                        rs.Value = task.Comments;
                        break;

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
            if (file == null)
            {
                Console.WriteLine("null");
            }
            else
            {
                var user = _unitOfWork.Users.GetBy(1);

                rs.Value = file.FileName;

                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "files/");
                string filePath = Path.Combine(uploadsFolder, file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                };
                Console.WriteLine(file.FileName + " " + id + " " + user.Username);
                _unitOfWork.AttachedFiles.Add(user, new AttachedFile(id, file.FileName));
            }
            return rs;
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