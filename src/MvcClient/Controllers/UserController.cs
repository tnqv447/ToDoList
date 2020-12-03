using AppCore.Interfaces;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcClient.Models;

namespace MvcClient.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<User> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork unitOfWork,
                              ILogger<User> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var users = _unitOfWork.Users.GetAll();
            // var roles = _unitOfWork.
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
            User source = this._unitOfWork.Users.GetBy(6);

            // MyEnum myEnum = (MyEnum)Enum.Parse(typeof(MyEnum), myString);
            // (ROLE)Enum.parse(typeof(ROLE), 'WORKER')
            if (ModelState.IsValid)
            {
                this._unitOfWork.Users.Add(source, user);
            }
            return View();
        }
        public IActionResult Detail(int id)
        {
            var model = new UserModel();
            model.User = this._unitOfWork.Users.GetBy(id);
            return View(model);
        }
        public IActionResult Update(int id)
        {
            var model = new UserModel();
            model.User = this._unitOfWork.Users.GetBy(id);
            return View(model);
        }
    }
}