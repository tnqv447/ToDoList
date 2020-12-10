using System;
using System.Collections.Generic;
using System.Linq;
using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Repositories
{
    public class UserRepos : Repository<User>, IUserRepos
    {
        private readonly ToDoListContext _context;
        public UserRepos(ToDoListContext context) : base(context)
        {
            _context = context;
        }

        public User GetUserByAccount(string username, string password)
        {
            var users = _context.Users.Where(m => m.Username.Equals(username) && m.Password.Equals(password));
            if (users.Any()) return users.First();
            return null;
        }
        public IList<User> GetUserNotJointForTask(int taskId)
        {
            var arr = _context.JointUsers.Where(m => m.ToDoTaskId.Equals(taskId)).Select(m => m.User);
            return this.GetAll().Except(arr).ToList();
        }
        public bool isUserNameExists(string username)
        {
            return _context.Users.Select(m => m.Username).Where(m => m.Equals(username)).Any();
        }

        public void Activate(User source, User entity)
        {
            this.UpdateStatus(source, entity, USER_STATUS.ACTIVE);
        }

        public void Disable(User source, User entity)
        {
            this.UpdateStatus(source, entity, USER_STATUS.DISABLED);
        }

        private void UpdateStatus(User source, User entity, USER_STATUS status)
        {
            entity.Status = status;
            this.Update(null, entity);

            var log = new DbLog(DateTime.Now, ACTION_TARGET.USER, ACTION.CHANGE_STATUS, CHANGE_FIELD.TITLE, source.Id, entity.Id, entity.Name, EnumConverter.Convert(status), null);
            _context.DbLogs.Add(log);
            _context.SaveChanges();
        }
        public IList<int> GetListId()
        {
            var result = _context.Users.Select(m => m.Id).ToList();
            return result;
        }
        public IList<string> GetListName()
        {
            var result = _context.Users.Select(m => m.Name).ToList();
            return result;
        }
    }
}