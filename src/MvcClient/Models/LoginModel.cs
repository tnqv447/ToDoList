using System;
namespace MvcClient.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Message { get; set; } = "";
    }
}