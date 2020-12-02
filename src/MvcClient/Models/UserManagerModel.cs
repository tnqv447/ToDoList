using System;
using System.Collections.Generic;
using AppCore.Models;

namespace MvcClient.Models
{
    public class UserManagerModel
    {
        public IList<User> Users { get; set; }
        public User NewUser { get; set; }
        public IList<string> Roles { get; set; }
        public UserManagerModel()
        {
            NewUser = new User();
        }
        public UserManagerModel(IList<User> users, IList<string> roles)
        {
            Users = users;
            Roles = roles;
            NewUser = new User();
        }
    }
}