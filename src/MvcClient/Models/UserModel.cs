using System;
using System.Collections.Generic;
using AppCore.Models;
namespace MvcClient.Models
{
    public class UserModel
    {
        public User user { get; set; }
        public IList<ROLE> Roles { get; set; }
    }
}