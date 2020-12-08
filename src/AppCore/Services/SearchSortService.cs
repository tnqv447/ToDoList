using System.Diagnostics.Tracing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AppCore.Models;

namespace AppCore.Services
{
    public class SearchSortService : ISearchSortService
    {
        //search
        public IList<User> Search(IList<User> users, string searchString, ROLE? role, USER_STATUS status = USER_STATUS.ALL, SEARCH_SORT_TYPE type = SEARCH_SORT_TYPE.NAME)
        {
            var arr = users.Select(m => new User(m, m.Id)).ToList();
            if (!status.Equals(USER_STATUS.ALL))
            {
                arr = arr.Where(m => m.Status.Equals(status)).ToList();
            }
            switch (type)
            {
                case SEARCH_SORT_TYPE.ROLE:
                    if (role == null) break;
                    users = users.Where(m => m.Role.Equals(role)).ToList();
                    break;
                default:
                    users = users.Where(m => m.Name.Contains(searchString.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
            }
            return arr;
        }
        public IList<ToDoTask> Search(IList<ToDoTask> tasks, string searchString, SCOPE? scope, STATUS status = STATUS.ALL, SEARCH_SORT_TYPE type = SEARCH_SORT_TYPE.NAME)
        {
            var arr = tasks;
            if (!status.Equals(STATUS.ALL))
            {
                arr = arr.Where(m => m.Status.Equals(status)).ToList();
            }
            switch (type)
            {
                case SEARCH_SORT_TYPE.SCOPE:
                    if (scope == null) break;
                    arr = arr.Where(m => m.Scope.Equals(scope)).ToList();
                    break;
                default:
                    arr = arr.Where(m => m.Title.Contains(searchString.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
            }
            return arr;
        }
        public IList<DbLog> Search(IList<DbLog> logs, string searchString, int? searchId, ACTION? action, DateTime execDate, SEARCH_SORT_TYPE type = SEARCH_SORT_TYPE.ACTION)
        {
            var arr = logs.Select(m => new DbLog(m, m.Id)).ToList();

            switch (type)
            {
                case SEARCH_SORT_TYPE.EXEC_USER_NAME:
                    logs = logs.Where(m => m.ExecUserName.Contains(searchString.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case SEARCH_SORT_TYPE.TASK_NAME:
                    logs = logs.Where(m => !m.ActionTarget.Equals(ACTION_TARGET.USER)
                       && m.TargetName.Contains(searchString.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
                    break;
                case SEARCH_SORT_TYPE.EXEC_USER_ID:
                    if (searchId == null) break;
                    logs = logs.Where(m => m.ExecUserId.Equals(searchId)).ToList();
                    break;
                case SEARCH_SORT_TYPE.TASK_ID:
                    if (searchId == null) break;
                    logs = logs.Where(m => !m.ActionTarget.Equals(ACTION_TARGET.USER) && m.TargetId.Equals(searchId)).ToList();
                    break;
                case SEARCH_SORT_TYPE.EXEC_DATE:
                    logs = logs.Where(m => DateTime.Compare(m.ExecDate, execDate) >= 0).ToList();
                    break;
                default:
                    if (action == null) break;
                    logs = logs.Where(m => m.Action.Equals(action)).ToList();
                    break;
            }

            return arr;
        }

        //sort
        public void Sort(IList<User> users, SEARCH_SORT_TYPE type = SEARCH_SORT_TYPE.ID, SORT_ORDER order = SORT_ORDER.ASCENDING)
        {
            switch (type)
            {
                case SEARCH_SORT_TYPE.NAME:
                    users = users.OrderBy(m => m.Name).ToList();
                    break;
                case SEARCH_SORT_TYPE.ROLE:
                    users = users.OrderBy(m => m.Role).ToList();
                    break;
                case SEARCH_SORT_TYPE.STATUS:
                    users = users.OrderBy(m => m.Status).ToList();
                    break;
                default:
                    users = users.OrderBy(m => m.Id).ToList();
                    break;
            }
            if (order.Equals(SORT_ORDER.DESCENDING)) users = users.Reverse().ToList();
        }
        public void Sort(IList<ToDoTask> tasks, SEARCH_SORT_TYPE type = SEARCH_SORT_TYPE.ID, SORT_ORDER order = SORT_ORDER.ASCENDING)
        {
            switch (type)
            {
                case SEARCH_SORT_TYPE.NAME:
                    tasks = tasks.OrderBy(m => m.Title).ToList();
                    break;
                case SEARCH_SORT_TYPE.SCOPE:
                    tasks = tasks.OrderBy(m => m.Scope).ToList();
                    break;
                case SEARCH_SORT_TYPE.STATUS:
                    tasks = tasks.OrderBy(m => m.Status).ToList();
                    break;
                default:
                    tasks = tasks.OrderBy(m => m.Id).ToList();
                    break;
            }
            if (order.Equals(SORT_ORDER.DESCENDING)) tasks = tasks.Reverse().ToList();
        }

        public void Sort(IList<DbLog> logs, SORT_ORDER order = SORT_ORDER.ASCENDING)
        {
            logs = logs.OrderBy(m => m.ExecDate).ToList();
            if (order.Equals(SORT_ORDER.DESCENDING)) logs = logs.Reverse().ToList();
        }
    }
}