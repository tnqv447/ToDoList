using System;
using System.Collections.Generic;
using AppCore.Models;

namespace MvcClient.Models
{
    public class UserManagerModel
    {
        public PaginatedList<User> Users { get; set; }
        public IList<ROLE> Roles { get; set; }
        public string SearchName { get; set; }
        public UserManagerModel()
        {
        }
    }
}