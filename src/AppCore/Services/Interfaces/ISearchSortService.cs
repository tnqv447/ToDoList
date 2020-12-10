using System;
using System.Collections.Generic;
using AppCore.Models;

namespace AppCore.Services
{
    public interface ISearchSortService
    {
        //search
        IList<User> Search(IList<User> users, string searchString, ROLE? role, USER_STATUS status = USER_STATUS.ALL, SEARCH_SORT_TYPE type = SEARCH_SORT_TYPE.NAME);
        IList<ToDoTask> Search(IList<ToDoTask> tasks, string searchString, SCOPE? action, STATUS status = STATUS.ALL, SEARCH_SORT_TYPE type = SEARCH_SORT_TYPE.NAME);
        IList<DbLog> Search(IList<DbLog> logs, string searchString, int? searchId, ACTION? action, DateTime execDate, SEARCH_SORT_TYPE type = SEARCH_SORT_TYPE.ACTION);

        //sort
        IList<User> Sort(IList<User> users, SEARCH_SORT_TYPE type = SEARCH_SORT_TYPE.ID, SORT_ORDER order = SORT_ORDER.ASCENDING);
        IList<ToDoTask> Sort(IList<ToDoTask> tasks, SEARCH_SORT_TYPE type = SEARCH_SORT_TYPE.ID, SORT_ORDER order = SORT_ORDER.ASCENDING);
        IList<DbLog> Sort(IList<DbLog> logs, SORT_ORDER order = SORT_ORDER.ASCENDING);
    }
}