using System;
using System.Collections.Generic;
using AppCore.Models;

namespace MvcClient.Models
{
    public class UserManagerModel
    {
        public IList<User> Users { get; set; }
        public IList<ROLE> Roles { get; set; }
        public string SearchName { get; set; }
        public UserManagerModel()
        {
        }
        public UserManagerModel(IList<User> users, IList<ROLE> roles)
        {
            Users = users;
            Roles = roles;
        }
    }
}