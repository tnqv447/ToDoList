using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCore.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Không được để trống tên")]
        public string Name { get; set; }

        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Số điện thoại phải có 10 chữ số")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public ROLE Role { get; set; }
        public USER_STATUS Status { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]+@[a-zA-Z]+.[a-zA-Z]+$", ErrorMessage = "Email ko hợp lệ")]
        [Required(ErrorMessage = "Không được để trống tên tài khoản")]
        public string Username { get; set; }
        [MinLength(5, ErrorMessage = "Phải có ít nhất 5 ký tự")]
        [Required(ErrorMessage = "Không được để trống mật khẩu")]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Không được để trống mật khẩu")]
        [Compare(nameof(Password), ErrorMessage = "Không khớp với mật khẩu.")]
        public string ConfirmPassword { get; set; }


        public virtual IList<JointUser> JointUsers { get; set; }
        public virtual IList<DbLog> DbLogs { get; set; }
        public virtual IList<ToDoTask> ToDoTasks { get; set; }

        [NotMapped]
        public string RoleName { get { return EnumConverter.Convert(this.Role); } }
        [NotMapped]
        public string StatusName { get { return EnumConverter.Convert(this.Status); } }

        public User() { }
        public User(User user)
        {
            this.Copy(user);
        }
        public User(User user, int Id)
        {
            this.Copy(user);
            this.Id = Id;
        }
        public User(string name, string phoneNumber, string address, ROLE role, USER_STATUS status, string username, string password)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
            Role = role;
            Status = status;
            Username = username;
            Password = password;
        }

        public void Copy(User user)
        {
            Name = user.Name;
            PhoneNumber = user.PhoneNumber;
            Address = user.Address;
            Role = user.Role;
            Status = user.Status;
            Username = user.Username;
            Password = user.Password;
        }
    }
}