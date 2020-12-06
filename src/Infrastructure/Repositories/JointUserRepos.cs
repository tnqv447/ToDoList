using System.Collections.Generic;
using System.Linq;
using AppCore.Interfaces;
using AppCore.Models;

namespace Infrastructure.Repositories
{
    public class JointUserRepos : Repository<JointUser>, IJointUserRepos
    {
        private readonly ToDoListContext _context;
        public JointUserRepos(ToDoListContext context) : base(context)
        {
            _context = context;
        }

        public JointUser GetByUserAndTask(int usersId, int taskId){
            var arr =  _context.JointUsers.Where(m => m.UserId.Equals(usersId) && m.ToDoTaskId.Equals(taskId));
            if(arr == null || arr.Count() == 0 ) return null;
            else return arr.First();
        }

        
    }
}