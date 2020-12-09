using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace MvcClient.Models
{
    public class UserModel
    {
        public User User { get; set; }
        public IList<ROLE> Roles { get; set; }
    }
    public enum Role
    {
        [Display(Name = "Nhân viên")]
        WORKER,
        [Display(Name = "Quản lý")]
        MANAGER
    }
    public enum UserStatus
    {
        [Display(Name = "Kích hoạt")]
        ACTIVE,
        [Display(Name = "Khóa")]
        DISABLED
    }
}