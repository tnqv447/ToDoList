using System.Linq;
using AppCore.Models;
using System;
namespace Infrastructure
{
    public class DataSeeds
    {
        public static void Initialize(ToDoListContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User("Nguyễn Văn A", "0123456789", "sdadad", ROLE.WORKER, USER_STATUS.ACTIVE, "nv1", "12345"),
                    new User("Nguyễn Văn B", "0123456789", "sdadad", ROLE.WORKER, USER_STATUS.ACTIVE, "nv2", "12345"),
                    new User("Nguyễn Văn C", "0123456789", "sdadad", ROLE.WORKER, USER_STATUS.ACTIVE, "nv3", "12345"),
                    new User("Trần thị Z", "0123456789", "sdadad", ROLE.WORKER, USER_STATUS.ACTIVE, "nv4", "12345"),
                    new User("Trần thị Y", "0123456789", "sdadad", ROLE.WORKER, USER_STATUS.ACTIVE, "nv5", "12345"),
                    new User("Nguyễn Văn D", "0123456789", "sdadad", ROLE.MANAGER, USER_STATUS.ACTIVE, "ql1", "12345"),
                    new User("Nguyễn Văn E", "0123456789", "sdadad", ROLE.MANAGER, USER_STATUS.ACTIVE, "ql2", "12345")
                );
                context.SaveChanges();
            }
            if (!context.ToDoTasks.Any())
            {
                context.ToDoTasks.AddRange(
                    new ToDoTask(
                        "Công việc 1",
                        "asdasdasd",
                        DateTime.ParseExact("01/01/2020", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        DateTime.ParseExact("12/01/2020", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        1,
                        STATUS.ON_PROGRESS,
                        SCOPE.PUBLIC),
                    new ToDoTask(
                        "Công việc 2",
                        "asdasdasd",
                        DateTime.ParseExact("01/01/2020", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        DateTime.ParseExact("13/01/2020", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        2,
                        STATUS.ON_PROGRESS,
                        SCOPE.PUBLIC),
                    new ToDoTask(
                        "Công việc 3",
                        "asdasdasd",
                        DateTime.ParseExact("01/01/2020", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        DateTime.ParseExact("14/01/2020", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        3,
                        STATUS.NEW,
                        SCOPE.PUBLIC),
                    new ToDoTask(
                        "Công việc 4",
                        "asdasdasd",
                        DateTime.ParseExact("01/01/2020", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        DateTime.ParseExact("15/01/2020", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                        1,
                        STATUS.NEW,
                        SCOPE.PUBLIC)
                );
                // if(!context.)
                context.SaveChanges();
            }
            if (!context.JointUsers.Any())
            {
                context.JointUsers.AddRange(
                    new JointUser(2, 1),
                    new JointUser(4, 1),
                    new JointUser(1, 2),
                    new JointUser(1, 3),
                    new JointUser(4, 3),
                    new JointUser(5, 3),
                    new JointUser(4, 4),
                    new JointUser(2, 3)
                );
                context.SaveChanges();
            }
            if (!context.Comments.Any())
            {
                context.Comments.AddRange(
                    new Comment(1, 1, DateTime.ParseExact("02/01/2020", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), "hello"),
                    new Comment(1, 2, DateTime.ParseExact("02/01/2020", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), "bell"),
                    new Comment(1, 4, DateTime.ParseExact("03/01/2020", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), "bell2"),
                    new Comment(3, 1, DateTime.ParseExact("04/01/2020", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), "hi"),
                    new Comment(3, 3, DateTime.ParseExact("05/01/2020", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture), "getout")
                );
                context.SaveChanges();
            }
        }
    }
}