using AppCore.Interfaces;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcClient.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace MvcClient.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<User> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public LoginController(ILogger<User> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("id") == null)
            {
                var model = new LoginModel();
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public IActionResult Index(LoginModel model)
        {
            string user = model.Username.Trim();
            string pass = model.Password.Trim();
            ViewResult view = View();
            if (this._unitOfWork.Users.isUserNameExists(user))
            {
                User account = this._unitOfWork.Users.GetUserByAccount(user, pass);
                if (account == null)
                {
                    model.Message = "Tài khoản hoặc mật khẩu bị sai.";
                    view = View(model);
                }
                else
                {
                    HttpContext.Session.SetInt32("id", account.Id);
                    HttpContext.Session.SetString("name", account.Name);
                    if (account.Role == ROLE.MANAGER)
                    {
                        HttpContext.Session.SetString("role", "manager");
                        HttpContext.Session.SetInt32("isManager", 1);
                        HttpContext.Session.SetInt32("isWorker", 0);
                    }

                    else
                    {
                        HttpContext.Session.SetString("role", "worker");
                        HttpContext.Session.SetInt32("isManager", 0);
                        HttpContext.Session.SetInt32("isWorker", 1);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                model.Message = "Tài khoản này không tồn tại.";
                view = View(model);
            }
            return view;
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Index)); // return to index
        }
        // t nói r, do đã login nó chứa session, nên sẽ thành infinity loop nếu cứ direct về login/index, phải clear session

    }
}