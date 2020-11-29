using System.Collections.Generic;
using AppCore.Models;

namespace AppCore.Services
{
    public interface ISearchSortService
    {
        //search
        IList<User> Search(IList<User> users, string searchString, USER_STATUS status = USER_STATUS.ALL, SEARCH_SORT_TYPE type = SEARCH_SORT_TYPE.NAME);
        IList<ToDoTask> Search(IList<ToDoTask> tasks, string searchString, STATUS status = STATUS.ALL, SEARCH_SORT_TYPE type = SEARCH_SORT_TYPE.NAME);

        //sort
        void Sort(IList<User> users, SEARCH_SORT_TYPE type = SEARCH_SORT_TYPE.ID, SORT_ORDER order = SORT_ORDER.ASCENDING);
        void Sort(IList<ToDoTask> tasks, SEARCH_SORT_TYPE type = SEARCH_SORT_TYPE.ID, SORT_ORDER order = SORT_ORDER.ASCENDING);
    }
}