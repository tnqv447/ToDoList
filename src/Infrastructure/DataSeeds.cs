using System.Linq;
using AppCore.Models;

namespace Infrastructure
{
    public class DataSeeds
    {
        public static void Initialize(ToDoListContext context)
        {
            context.Database.EnsureCreated();

            if(!context.Users.Any())
            {
                context.Users.AddRange(
                    new User("Nguyễn Văn A", "0123456789", "sdadad", ROLE.WORKER, USER_STATUS.ACTIVE, "nv1", "12345"),
                    new User("Nguyễn Văn B", "0123456789", "sdadad", ROLE.WORKER, USER_STATUS.ACTIVE, "nv2", "12345"),
                    new User("Nguyễn Văn C", "0123456789", "sdadad", ROLE.WORKER, USER_STATUS.ACTIVE, "nv3", "12345"),
                    new User("Trần thị Z", "0123456789", "sdadad", ROLE.WORKER, USER_STATUS.ACTIVE, "nv3", "12345"),
                    new User("Trần thị Y", "0123456789", "sdadad", ROLE.WORKER, USER_STATUS.ACTIVE, "nv3", "12345"),
                    new User("Nguyễn Văn D", "0123456789", "sdadad", ROLE.MANAGER, USER_STATUS.ACTIVE, "ql1", "12345"),
                    new User("Nguyễn Văn E", "0123456789", "sdadad", ROLE.MANAGER, USER_STATUS.ACTIVE, "ql2", "12345")
                );
            }
        }
    }
}