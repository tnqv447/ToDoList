using System.Collections.Generic;
using AppCore.Interfaces;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcClient.Models;

namespace MvcClient.Controllers
{
    public class LogController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private IList<DbLog> logs;
        private LogViewModel view;
        public LogController(IUnitOfWork unitOfWork, ILogger<HomeController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;

        }
        public IActionResult Index()
        {
            logs = _unitOfWork.DbLogs.GetAll();
            view = new LogViewModel();
            view.Logs = logs;
            return View(view);
        }
    }
}