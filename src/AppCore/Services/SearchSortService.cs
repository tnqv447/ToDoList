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
        public IList<User> Search(IList<User> users, string searchString, USER_STATUS status = USER_STATUS.ALL, SEARCH_SORT_TYPE type = SEARCH_SORT_TYPE.NAME)
        {
            var arr = users.Select(m => new User(m, m.Id)).ToList();
            if (!status.Equals(USER_STATUS.ALL))
            {
                arr = arr.Where(m => m.Status.Equals(status)).ToList();
            }
            switch (type)
            {
                case SEARCH_SORT_TYPE.ROLE: users = users.OrderBy(m => m.Name).ToList(); break;
                default: users = users.Where(m => m.Name.Contains(searchString.Trim(), StringComparison.OrdinalIgnoreCase)).ToList(); break;
            }
            return arr;
        }
        public IList<ToDoTask> Search(IList<ToDoTask> tasks, string searchString, STATUS status = STATUS.ALL, SEARCH_SORT_TYPE type = SEARCH_SORT_TYPE.NAME)
        {
            // var arr = tasks.Select(m => new ToDoTask(m, m.Id)).ToList();
            var arr = tasks;
            if (!status.Equals(STATUS.ALL))
            {
                arr = arr.Where(m => m.Status.Equals(status)).ToList();
            }
            switch (type)
            {
                case SEARCH_SORT_TYPE.SCOPE: arr = arr.OrderBy(m => m.Scope).ToList(); break;
                default: arr = arr.Where(m => m.Title.Contains(searchString.Trim(), StringComparison.OrdinalIgnoreCase)).ToList(); break;
            }

            return arr;
        }

        //sort
        public void Sort(IList<User> users, SEARCH_SORT_TYPE type = SEARCH_SORT_TYPE.ID, SORT_ORDER order = SORT_ORDER.ASCENDING)
        {
            switch (type)
            {
                case SEARCH_SORT_TYPE.NAME: users = users.OrderBy(m => m.Name).ToList(); break;
                case SEARCH_SORT_TYPE.ROLE: users = users.OrderBy(m => m.Role).ToList(); break;
                case SEARCH_SORT_TYPE.STATUS: users = users.OrderBy(m => m.Status).ToList(); break;
                default: users = users.OrderBy(m => m.Id).ToList(); break;
            }
            if (order.Equals(SORT_ORDER.DESCENDING)) users = users.Reverse().ToList();
        }
        public void Sort(IList<ToDoTask> tasks, SEARCH_SORT_TYPE type = SEARCH_SORT_TYPE.ID, SORT_ORDER order = SORT_ORDER.ASCENDING)
        {
            switch (type)
            {
                case SEARCH_SORT_TYPE.NAME: tasks = tasks.OrderBy(m => m.Title).ToList(); break;
                case SEARCH_SORT_TYPE.SCOPE: tasks = tasks.OrderBy(m => m.Scope).ToList(); break;
                case SEARCH_SORT_TYPE.STATUS: tasks = tasks.OrderBy(m => m.Status).ToList(); break;
                default: tasks = tasks.OrderBy(m => m.Id).ToList(); break;
            }
            if (order.Equals(SORT_ORDER.DESCENDING)) tasks = tasks.Reverse().ToList();
        }
    }
}