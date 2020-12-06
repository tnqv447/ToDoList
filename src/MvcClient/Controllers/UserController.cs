using AppCore.Interfaces;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcClient.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace MvcClient.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<User> _logger;
        private readonly IUnitOfWork _unitOfWork;

        private User source = null;
        public ROLE Role { get; set; }
        public UserController(IUnitOfWork unitOfWork,
                              ILogger<User> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (this.isLogin() == false)
            {
                return RedirectToAction("Index", "Login");
            }
            this.LoginUser();
            var users = _unitOfWork.Users.GetAll();
            var model = new UserManagerModel();
            model.RoleUser = this.Role;
            model.Users = users;
            return View(model);
        }
        public IActionResult Create()
        {
            if (this.isLogin() == false)
            {
                return RedirectToAction("Index", "Login");
            }
            var model = new UserModel();
            model.RoleUser = this.Role;
            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserModel model)
        {
            User user = model.User;

            // MyEnum myEnum = (MyEnum)Enum.Parse(typeof(MyEnum), myString);
            // (ROLE)Enum.parse(typeof(ROLE), 'WORKER')
            if (ModelState.IsValid)
            {
                this._unitOfWork.Users.Add(source, user);
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Detail(int id)
        {
            if (this.isLogin() == false)
            {
                return RedirectToAction("Index", "Login");
            }
            var model = new UserModel();
            model.RoleUser = this.Role;
            model.User = this._unitOfWork.Users.GetBy(id);
            return View(model);
        }
        public IActionResult Update(int id)
        {
            if (this.isLogin() == false)
            {
                return RedirectToAction("Index", "Login");
            }
            var model = new UserModel();
            model.RoleUser = this.Role;
            model.User = this._unitOfWork.Users.GetBy(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(UserModel model)
        {
            User user = model.User;

            User oldUser = this._unitOfWork.Users.GetBy(user.Id);
            oldUser.Name = user.Name;
            oldUser.PhoneNumber = user.PhoneNumber;
            oldUser.Address = user.Address;
            oldUser.Role = user.Role;
            oldUser.Status = user.Status;

            if (ModelState.IsValid)
            {
                this._unitOfWork.Users.Update(source, oldUser);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Disable(UserModel model)
        {
            User user = this._unitOfWork.Users.GetBy(model.User.Id);
            this._unitOfWork.Users.Disable(source, user);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Active(UserModel model)
        {
            User user = this._unitOfWork.Users.GetBy(model.User.Id);
            this._unitOfWork.Users.Activate(source, user);
            return RedirectToAction(nameof(Index));
        }

        public bool isLogin()
        {
            if (HttpContext.Session.GetInt32("id") != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void LoginUser()
        {
            int id = HttpContext.Session.GetInt32("id").GetValueOrDefault();
            source = this._unitOfWork.Users.GetBy(id);
            if (HttpContext.Session.GetString("role").Equals("manager"))
            {
                this.Role = ROLE.MANAGER;
            }
            else
            {
                this.Role = ROLE.WORKER;
            }
        }
    }
}