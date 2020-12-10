using System;
using System.Collections.Generic;
using AppCore.Interfaces;
using AppCore.Models;
using AppCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private PaginatedList<DbLog> logPaging;
        public SelectList userIds;
        public SelectList taskIds;
        public SelectList userNames;
        private readonly ISearchSortService _service;
        public LogController(IUnitOfWork unitOfWork, ILogger<HomeController> logger, ISearchSortService service)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _service = service;
            userIds = new SelectList(_unitOfWork.Users.GetListId());
            taskIds = new SelectList(_unitOfWork.ToDoTasks.GetListId());
            userNames = new SelectList(_unitOfWork.Users.GetListName());
        }
        public IActionResult Index(int pageNumber = 1)
        {
            view = new LogViewModel();
            view = GetViewModel();
            return View(view);
        }
        public LogViewModel GetViewModel(int pageNumber = 1, string value = null, string action = null)
        {
            var pageSize = 10;
            LogViewModel result = new LogViewModel();
            IList<DbLog> temp = new List<DbLog>();
            logs = _unitOfWork.DbLogs.GetAll();
            if (value == null || action == null)
            {
                logs = _service.Sort(logs, SORT_ORDER.DESCENDING);
                logPaging = PaginatedList<DbLog>.Create(logs, pageNumber, pageSize);
            }
            else
            {
                switch (action)
                {
                    case "DateExec":
                        var date = DateTime.ParseExact(value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        temp = _service.Search(logs, null, null, null, date, SEARCH_SORT_TYPE.EXEC_DATE);
                        break;
                    case "TaskName":
                        temp = _service.Search(logs, value, null, null, DateTime.Now, SEARCH_SORT_TYPE.TASK_NAME);
                        break;
                    case "UserName":
                        temp = _service.Search(logs, value, null, null, DateTime.Now, SEARCH_SORT_TYPE.EXEC_USER_NAME);

                        break;
                    case "TaskId":
                        var id = Int32.Parse(value);
                        temp = _service.Search(logs, null, id, null, DateTime.Now, SEARCH_SORT_TYPE.TASK_ID);

                        break;
                    case "UserId":
                        var userId = Int32.Parse(value);
                        temp = _service.Search(logs, null, userId, null, DateTime.Now, SEARCH_SORT_TYPE.EXEC_USER_ID);

                        break;
                    case "Action":
                        var val_enum = Int32.Parse(value);
                        if (val_enum.Equals(0))
                        {
                            temp = _service.Search(logs, null, null, ACTION.ADD, DateTime.Now, SEARCH_SORT_TYPE.ACTION);
                        }
                        else
                        {
                            if (val_enum.Equals(1))
                            {
                                temp = _service.Search(logs, null, null, ACTION.DELETE, DateTime.Now, SEARCH_SORT_TYPE.ACTION);
                            }
                            else
                            {
                                if (val_enum.Equals(2))
                                {
                                    temp = _service.Search(logs, null, null, ACTION.UPDATE, DateTime.Now, SEARCH_SORT_TYPE.ACTION);
                                }
                                else
                                {
                                    if (val_enum.Equals(3))
                                    {
                                        temp = _service.Search(logs, null, null, ACTION.CHANGE_STATUS, DateTime.Now, SEARCH_SORT_TYPE.ACTION);
                                    }
                                    else
                                    {
                                        if (val_enum.Equals(4))
                                        {
                                            temp = _service.Search(logs, null, null, ACTION.CHANGE_SCOPE, DateTime.Now, SEARCH_SORT_TYPE.EXEC_DATE);
                                        }
                                    }
                                }
                            }
                        }

                        break;
                }

                temp = _service.Sort(temp, SORT_ORDER.DESCENDING);
                logPaging = PaginatedList<DbLog>.Create(temp, pageNumber, pageSize);

                // 1. Ngày thực hiện
                // 2.Tên công việc
                // 3. Người thực hiện
                // 4. ID công việc
                // 5. Id người thực hiện
                // 6. Hành động
            }
            result.Logs = logPaging;
            result.taskIds = taskIds;
            result.userIds = userIds;
            result.userNames = userNames;
            return result;
        }
        public IActionResult ItemPaging(int pageNumber, string value, string action)
        {
            view = GetViewModel(pageNumber, value, action);
            return PartialView("_LogList", view);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FilterLog(string value, string action)
        {
            view = GetViewModel(1, value, action);
            return PartialView("_LogList", view);
        }
    }
}