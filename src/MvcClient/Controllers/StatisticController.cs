using AppCore.Interfaces;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcClient.Models;
using Microsoft.AspNetCore.Http;
using System;
using AppCore.Services;

namespace MvcClient.Controllers
{
    public class StatisticController : Controller
    {
        private readonly ILogger<User> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAnalysisService _service;

        private DateTime startDate = DateTime.Now;
        private DateTime endDate = DateTime.Now.AddDays(7);

        public StatisticController(ILogger<User> logger, IUnitOfWork unitOfWork, IAnalysisService service)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _service = service;
        }
        public IActionResult Index()
        {
            var model = new StatisticModel();
            model.StartDate = this.startDate;
            model.EndDate = this.endDate;
            model.Analyze = this._service.AnalyzeByTasks(model.StartDate, model.EndDate);
            model.Users = this._unitOfWork.Users.GetAll();
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(StatisticModel model)
        {
            this.startDate = model.StartDate;
            this.endDate = model.EndDate;
            model.Analyze = this._service.AnalyzeByTasks(model.StartDate, model.EndDate);
            model.Users = this._unitOfWork.Users.GetAll();
            return View(model);
        }

        public IActionResult UserStatistic(int id)
        {
            var model = new StatisticModel();
            var user = _unitOfWork.Users.GetBy(id);
            model.Tasks = user.ToDoTasks;
            model.UserName = user.Name;
            model.id = id;
            model.StartDate = this.startDate;
            model.EndDate = this.endDate;
            model.Analyze = this._service.AnalyzeByUser(user, model.StartDate, model.EndDate);

            return View(model);
        }
        [HttpPost]
        public IActionResult UserStatistic(StatisticModel model)
        {
            var user = _unitOfWork.Users.GetBy(model.id);
            model.Tasks = user.ToDoTasks;
            model.UserName = user.Name;
            model.Analyze = this._service.AnalyzeByUser(user, model.StartDate, model.EndDate);
            return View(model);
        }
    }
}