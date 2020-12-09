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
        public UserController(IUnitOfWork unitOfWork,
                              ILogger<User> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var users = _unitOfWork.Users.GetAll();
            var model = new UserManagerModel();
            model.Users = users;
            return View(model);
        }
        public IActionResult Create()
        {
            var model = new UserModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserModel model)
        {
            User user = model.User;
            if (ModelState.IsValid)
            {
                this._unitOfWork.Users.Add(source, user);
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                Forbid();
            }
            int uid = id.GetValueOrDefault();
            var model = new UserModel();
            model.User = this._unitOfWork.Users.GetBy(uid);
            return View(model);
        }
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                Forbid();
            }
            int uid = id.GetValueOrDefault();
            var model = new UserModel();
            model.User = this._unitOfWork.Users.GetBy(uid);
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
                ViewBag.Message = "Cập nhật thông tin nhân viên " + oldUser.Name + " thành công!";
            }
            return View();
        }

        [HttpPost]
        public IActionResult Disable(UserModel model)
        {
            User user = this._unitOfWork.Users.GetBy(model.User.Id);
            this._unitOfWork.Users.Disable(source, user);
            ViewBag.Message = "Khóa nhân viên " + user.Name + " thành công!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Active(UserModel model)
        {
            User user = this._unitOfWork.Users.GetBy(model.User.Id);
            this._unitOfWork.Users.Activate(source, user);
            ViewBag.Message = "Mở khóa nhân viên " + user.Name + " thành công!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Profile(int? id)
        {
            if (id == null)
            {
                Forbid();
            }
            int uid = id.GetValueOrDefault();
            var model = new UserModel();
            model.User = this._unitOfWork.Users.GetBy(uid);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Profile(UserModel model)
        {
            User user = model.User;

            User oldUser = this._unitOfWork.Users.GetBy(user.Id);
            oldUser.Name = user.Name;
            oldUser.PhoneNumber = user.PhoneNumber;
            oldUser.Address = user.Address;

            if (ModelState.IsValid)
            {
                this._unitOfWork.Users.Update(source, oldUser);
                HttpContext.Session.SetString("name", oldUser.Name);
                model.User = oldUser;
                ViewBag.Message = "Cập nhật thông tin " + oldUser.Name + " thành công!";
            }
            return View(model);
        }
    }
}