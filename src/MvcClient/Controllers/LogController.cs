using AppCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MvcClient.Controllers
{
    public class LogController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public LogController(IUnitOfWork unitOfWork, ILogger<HomeController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}