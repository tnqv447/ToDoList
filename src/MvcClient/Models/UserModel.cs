using System;
using System.Collections.Generic;
using AppCore.Models;
namespace MvcClient.Models
{
    public class UserModel : CommonModel
    {
        public User User { get; set; }
        public IList<ROLE> Roles { get; set; }
    }
}